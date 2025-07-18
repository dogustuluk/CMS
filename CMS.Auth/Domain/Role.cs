using CMS.Auth.Domain.Enums;

namespace CMS.Auth.Domain;

public class Role
{
    public Guid Id { get; set; }
    public string RoleName { get; set; }
    public RoleLevel RoleLevel { get; set; }
    //role permission sonra eklenecek
}
