using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace CMS.Auth.Infrastructure.Configurations;

public static class RateLimitPolicyManager
{
    public static void AddDynamicRateLimitPolicy(this RateLimiterOptions opt, string endpointName, int limitPerWindow = 5, int windowsInSeconds = 60, int queueLimit = 0)
    {
        opt.AddFixedWindowLimiter(policyName: endpointName, configureOptions: options =>
        {
            options.PermitLimit = limitPerWindow;
            options.Window = TimeSpan.FromSeconds(windowsInSeconds);
            options.QueueLimit = queueLimit;
            options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;

        });
    }
}
