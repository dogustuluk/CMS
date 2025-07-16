using CMS.Auth.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CMS.Auth.Features.ValidateTenant;

public class ValidateTenantHandler : IRequestHandler<ValidateTenantCommand, ValidateTenantResponse>
{
    private readonly AppDbContext _appDbContext;

    public ValidateTenantHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<ValidateTenantResponse> Handle(ValidateTenantCommand request, CancellationToken cancellationToken)
    {
        var checkTenant = await _appDbContext.Tenants.AnyAsync(a => a.Id == request.TenantId);

        return new ValidateTenantResponse(checkTenant);
    }
}
