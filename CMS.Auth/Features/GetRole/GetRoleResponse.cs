using CMS.Auth.Domain.Enums;

namespace CMS.Auth.Features.GetRole;

public record GetRoleResponse(Guid Id, string RoleName, RoleLevel RoleLevel);
