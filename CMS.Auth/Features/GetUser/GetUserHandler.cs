using CMS.Auth.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CMS.Auth.Features.GetUser;

public class GetUserHandler : IRequestHandler<GetUserCommand, GetUserResponse>
{
    private readonly AppDbContext _appDbContext;

    public GetUserHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<GetUserResponse> Handle(GetUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _appDbContext.Users.FirstOrDefaultAsync(a => a.TenantId == request.TenantId);
        if (user == null)
            return new GetUserResponse(false);
        var tenantInfo = await _appDbContext.Tenants.FirstOrDefaultAsync(a => a.Id == user.TenantId);

        return new GetUserResponse(true, user.Id, user.UserName, user.Email, user.TenantId, tenantInfo.Domain, tenantInfo.Name);
    }
}
