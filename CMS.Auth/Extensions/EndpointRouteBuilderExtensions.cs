using MediatR;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;

namespace CMS.Auth.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static void MapPostEndpoint<TRequest, TResponse>(this WebApplication app, string endpoint, bool isHaveRateLimit = false, int limit = 5, int windowSeconds = 60, int queueLimit = 0) where TRequest : IRequest<TResponse>
    {
        var routeHandler = app.MapPost(endpoint, async (TRequest request, IMediator mediator) =>
        {
            var response = await mediator.Send(request);
            return Results.Json(response);
        });

        if (isHaveRateLimit)
        {
            string policyName = $"RateLimit__{endpoint.Replace("/", "_")}";

            var rateLimiterOptions = app.Services.GetRequiredService<IOptions<RateLimiterOptions>>().Value;
            rateLimiterOptions.AddFixedWindowLimiter(policyName, options =>
            {
                options.PermitLimit = limit;
                options.Window = TimeSpan.FromSeconds(windowSeconds);
                options.QueueLimit = queueLimit;
                options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
            });

            routeHandler.RequireRateLimiting(policyName);


        }

    }
}
