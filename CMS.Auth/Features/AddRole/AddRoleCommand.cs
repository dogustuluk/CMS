using CMS.Auth.Domain.Enums;
using MediatR;

namespace CMS.Auth.Features.AddRole;

public record AddRoleCommand(string RoleName, RoleLevel RoleLevel) : IRequest<AddRoleResponse>;
