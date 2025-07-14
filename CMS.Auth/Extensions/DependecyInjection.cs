using CMS.Auth.Features.Common;
using CMS.Auth.Features.Register;
using CMS.Auth.Infrastructure.Services;
using FluentValidation;
using MediatR;

namespace CMS.Auth.Extensions;

public static class DependecyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(RegisterUserHandler).Assembly);
        services.AddValidatorsFromAssembly(typeof(RegisterUserHandler).Assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
