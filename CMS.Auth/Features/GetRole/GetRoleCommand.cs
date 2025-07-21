using CMS.Auth.Domain.GenericObjects;
using MediatR;
using System.ComponentModel;

namespace CMS.Auth.Features.GetRole;

public record GetRoleCommand([DefaultValue(1)] int Page = 1, [DefaultValue(10)] int PageSize = 10) : IRequest<PaginatedList<GetRoleResponse>>;
