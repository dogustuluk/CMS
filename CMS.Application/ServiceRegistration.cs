using CMS.Application.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CMS.Application;
public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddScoped<JsonHelper>();

        return services;
    }
}
