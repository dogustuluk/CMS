using CMS.Auth.Domain;
using CMS.Auth.Features.Common;
using CMS.Auth.Features.Register;
using CMS.Auth.Infrastructure.Configurations;
using CMS.Auth.Infrastructure.Data;
using CMS.Auth.Infrastructure.Services;
using CMS.Auth.Tenant;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CMS.Auth.Extensions;

public static class DependecyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgreSQL"));
        });

        services.Configure<JWTSettings>(configuration.GetSection("Token"));

        services.AddMediatR(typeof(RegisterUserHandler).Assembly);
        services.AddValidatorsFromAssembly(typeof(RegisterUserHandler).Assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        services.AddScoped<ITenantAccessor, TenantAccessor>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenHandler, TokenHandler>();
        services.AddScoped<IClaimsFactory<User>, UserClaimsFactory>();

        return services;
    }
}
