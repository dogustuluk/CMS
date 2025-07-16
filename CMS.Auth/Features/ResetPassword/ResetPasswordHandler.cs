using CMS.Auth.Infrastructure.Data;
using CMS.Auth.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CMS.Auth.Features.ResetPassword;

public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, ResetPasswordResponse>
{
    private readonly AppDbContext _appDbContext;
    private readonly IPasswordHasher _passwordHasher;

    public ResetPasswordHandler(AppDbContext appDbContext, IPasswordHasher passwordHasher)
    {
        _appDbContext = appDbContext;
        _passwordHasher = passwordHasher;
    }

    public async Task<ResetPasswordResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _appDbContext.Users.FirstOrDefaultAsync(a => a.Id == request.UserId);
        if (user == null)
            return new ResetPasswordResponse(false, "Kullanıcı Bulunamadı");

        var checkPassword = _passwordHasher.Verify(request.OldPassword, user.PasswordHash);
        if (!checkPassword)
            return new ResetPasswordResponse(false, "Girilen şifreyi kontrol edin");

        var newHashedPassword = _passwordHasher.Hash(request.NewPassword);

        user.PasswordHash = newHashedPassword;
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return new ResetPasswordResponse(true, "Şifre Başarılı Bir Şekilde Değiştirildi");
    }
}
