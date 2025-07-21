using MediatR;

namespace CMS.Application.Features.Queries.GetPaginatedRole;
public record GetPaginatedRoleCommand(int Page = 1, int PageSize = 10) : IRequest<GetPaginatedRoleResponse>;
