using CMS.Auth.Infrastructure.Data;
using CMS.Auth.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CMS.Auth.Features.RefreshToken;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    private readonly AppDbContext _appDbContext;
    private readonly ITokenHandler _tokenHandler;

    public RefreshTokenHandler(AppDbContext appDbContext, ITokenHandler tokenHandler)
    {
        _appDbContext = appDbContext;
        _tokenHandler = tokenHandler;
    }

    public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _appDbContext.Users.FirstOrDefaultAsync(a => a.Id == request.UserId && a.RefreshToken == request.RefreshToken);
        if (user == null)
            return new RefreshTokenResponse(false, "Kullanıcı Bulunamadı");

        var token = _tokenHandler.GenerateToken(user);

        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpiresAt = token.ExpiresAt.AddDays(7);
        await _appDbContext.SaveChangesAsync();

        return new RefreshTokenResponse(true, "Token Yenilendi", token.AccessToken, user.Id);
    }
}
