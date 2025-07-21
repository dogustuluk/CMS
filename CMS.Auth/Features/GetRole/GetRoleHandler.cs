using CMS.Auth.Domain.DTOs;
using CMS.Auth.Domain.GenericObjects;
using CMS.Auth.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CMS.Auth.Features.GetRole;

public class GetRoleHandler : IRequestHandler<GetRoleCommand, PaginatedList<GetRoleResponse>>
{
    private readonly AppDbContext _appDbContext;

    public GetRoleHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<PaginatedList<GetRoleResponse>> Handle(GetRoleCommand request, CancellationToken cancellationToken)
    {
        var query = _appDbContext.Roles
            .AsNoTracking()
            .OrderBy(r => r.RoleName)
            .Select(r => new Get_RoleDto
            {
                Id = r.Id,
                RoleName = r.RoleName,
                RoleLevel = r.RoleLevel
            });

        var pagedDto = await query.CreateAsync(request.Page, request.PageSize);

        var response = new PaginatedList<GetRoleResponse>
        {
            Pagination = pagedDto.Pagination,
            Data = pagedDto.Data?.Select(dto => new GetRoleResponse(
                Id: dto.Id,
                RoleName: dto.RoleName,
                RoleLevel: dto.RoleLevel
            )).ToList()
        };

        return response;
    }
}
