using FluentValidation;

namespace CMS.Auth.Features.Login;

public class AuthenticateUserValidator : AbstractValidator<AuthenticateUserCommand>
{
    public AuthenticateUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email Boş Olamaz!")
            .EmailAddress().WithMessage("Hatalı Email Adresi");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password Boş Olamaz!")
            .MinimumLength(6).WithMessage("Şifre Alanı Minimum 3 Karakter Olmalı");
    }
}
