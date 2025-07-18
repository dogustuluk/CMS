using FluentValidation;

namespace CMS.Auth.Features.AddRole;

public class AddRoleValidator : AbstractValidator<AddRoleCommand>
{
    public AddRoleValidator()
    {
        RuleFor(x => x.RoleName)
         .NotEmpty().WithMessage("Rol Adı Boş Olamaz!")
         .MinimumLength(3).WithMessage("Kullanıcı Adı Minimum 5 Karakter Olmalı");
    }
}
