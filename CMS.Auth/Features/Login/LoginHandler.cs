using CMS.Auth.Infrastructure.Data;
using CMS.Auth.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CMS.Auth.Features.Login;

public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly AppDbContext _appDbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenHandler _tokenHandler;

    public LoginHandler(AppDbContext appDbContext, IPasswordHasher passwordHasher, ITokenHandler tokenHandler)
    {
        _appDbContext = appDbContext;
        _passwordHasher = passwordHasher;
        _tokenHandler = tokenHandler;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _appDbContext.Users.FirstOrDefaultAsync(a => a.Email == request.Email, cancellationToken);
        if (user == null)
            return new LoginResponse(false, "Email ya da kullanıcı şifresi hatalı");

        var passStatus = _passwordHasher.Verify(request.Password, user.PasswordHash);
        if (passStatus == false)
            return new LoginResponse(false, "Email ya da kullanıcı şifresi hatalı");

        var tokenResult = _tokenHandler.GenerateToken(user);

        user.RefreshToken = tokenResult.RefreshToken;
        user.RefreshTokenExpiresAt = tokenResult.ExpiresAt.AddDays(2);

        await _appDbContext.SaveChangesAsync();

        return new LoginResponse(true, "Giriş başarılı", tokenResult.AccessToken, tokenResult.RefreshToken);
    }
}
