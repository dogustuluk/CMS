using FluentValidation;

namespace CMS.Auth.Features.ResetPassword;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.OldPassword)
            .NotEmpty().WithMessage("Şifre Boş Olamaz!")
            .MinimumLength(6).WithMessage("Şifre Alanı Minimum 3 Karakter Olmalı");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Yeni Şifre Boş Olamaz!")
            .MinimumLength(6).WithMessage("Şifre Alanı Minimum 3 Karakter Olmalı");

        RuleFor(x => x.ApplyNewPassword)
            .NotEmpty().WithMessage("Yeni Şifre (Tekrar) Boş Olamaz!")
            .MinimumLength(6).WithMessage("Şifre Alanı Minimum 3 Karakter Olmalı")
            .Equal(a => a.NewPassword).WithMessage("Yeni Şifreler Eşleşmiyor");
    }
}
