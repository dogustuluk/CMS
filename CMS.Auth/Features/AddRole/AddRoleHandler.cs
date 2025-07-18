using CMS.Auth.Domain;
using CMS.Auth.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CMS.Auth.Features.AddRole;

public class AddRoleHandler : IRequestHandler<AddRoleCommand, AddRoleResponse>
{
    private readonly AppDbContext _appDbContext;

    public AddRoleHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<AddRoleResponse> Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        var exist = await _appDbContext.Roles.AnyAsync(a => a.RoleName == request.RoleName && a.RoleLevel == request.RoleLevel);
        if (exist)
            return new AddRoleResponse(false, "Eklenmek istenen rol zaten kayıtlı.");

        var role = new Role
        {
            Id = Guid.NewGuid(),
            RoleLevel = request.RoleLevel,
            RoleName = request.RoleName
        };

        await _appDbContext.Roles.AddAsync(role);
        await _appDbContext.SaveChangesAsync();

        return new AddRoleResponse(true, "İşlem Başarılı");
    }
}
