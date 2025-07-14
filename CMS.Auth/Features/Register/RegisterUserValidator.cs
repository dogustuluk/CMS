using FluentValidation;

namespace CMS.Auth.Features.Register;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Kullanıcı Adı Boş Olamaz!")
            .MinimumLength(3).WithMessage("Kullanıcı Adı Minimum 3 Karakter Olmalı");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email Boş Olamaz!")
            .EmailAddress().WithMessage("Hatalı Email Adresi");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password Boş Olamaz!")
            .MinimumLength(6).WithMessage("Şifre Alanı Minimum 3 Karakter Olmalı");
    }
}
