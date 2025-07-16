using CMS.Application.Abstractions.Services;
using CMS.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CMS.Infrastructure;
public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddHttpClient<IAuthService, AuthService>();
        return services;
    }

}
