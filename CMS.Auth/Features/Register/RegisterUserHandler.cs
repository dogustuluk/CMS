using CMS.Auth.Domain;
using CMS.Auth.Infrastructure.Data;
using CMS.Auth.Infrastructure.Services;
using CMS.Auth.Tenant;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CMS.Auth.Features.Register;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly AppDbContext _appDbContext;
    private readonly ITenantAccessor _tenantAccessor;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserHandler(AppDbContext appDbContext, ITenantAccessor tenantAccessor, IPasswordHasher passwordHasher)
    {
        _appDbContext = appDbContext;
        _tenantAccessor = tenantAccessor;
        _passwordHasher = passwordHasher;
    }

    public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var exist = await _appDbContext.Users.AnyAsync(a => a.Email == request.Email && a.UserName == request.Username);
        if (exist)
            return new RegisterUserResponse(false, "Aynı Kişi Tekrar Kayıt Olamaz");

        var user = new User
        {
            Id = Guid.NewGuid(),
            TenantId = _tenantAccessor.TenantId,
            UserName = request.Username,
            Email = request.Email,
            PasswordHash = _passwordHasher.Hash(request.Password),
            IsActive = true
        };

        await _appDbContext.Users.AddAsync(user);
        await _appDbContext.SaveChangesAsync();

        return new RegisterUserResponse(true);
    }
}