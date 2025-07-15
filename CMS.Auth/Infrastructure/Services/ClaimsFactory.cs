using CMS.Auth.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CMS.Auth.Infrastructure.Services;

public interface IClaimsFactory<T>
{
    IEnumerable<Claim> CreateClaims(T entity);
}

public class UserClaimsFactory : IClaimsFactory<User>
{
    public IEnumerable<Claim> CreateClaims(User entity)
    {
        return new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, entity.Id.ToString()),
            new Claim("username", entity.UserName),
            new Claim("email", entity.Email),
            new Claim("tenantId", entity.TenantId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
    }
}
