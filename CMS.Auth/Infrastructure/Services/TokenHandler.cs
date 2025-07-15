using CMS.Auth.Domain;
using CMS.Auth.Domain.DTOs;
using CMS.Auth.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace CMS.Auth.Infrastructure.Services;

public interface ITokenHandler
{
    TokenResult GenerateToken(User user);
}

public class TokenHandler : ITokenHandler
{
    private readonly JWTSettings _jwtSettings;
    private readonly IClaimsFactory<User> _claimFactory;

    public TokenHandler(IOptions<JWTSettings> jwtOptions, IClaimsFactory<User> claimFactory)
    {
        _jwtSettings = jwtOptions.Value;
        _claimFactory = claimFactory;
    }

    public TokenResult GenerateToken(User user)
    {
        var claims = _claimFactory.CreateClaims(user);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes);

        var now = DateTime.UtcNow;


        var token = new JwtSecurityToken
            (
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expiresAt,
            notBefore: now.AddSeconds(-1),
            signingCredentials: credentials
            );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new TokenResult
        {
            AccessToken = tokenString,
            RefreshToken = GenerateRefreshToken(),
            ExpiresAt = expiresAt
        };

    }

    private static string GenerateRefreshToken()
    {
        var bytes = new Byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }
}
