using CMS.Auth.Domain.Enums;

namespace CMS.Auth.Domain.DTOs;

public class Get_RoleDto
{
    public Guid Id { get; set; }
    public string RoleName { get; set; }
    public RoleLevel RoleLevel { get; set; }
}
