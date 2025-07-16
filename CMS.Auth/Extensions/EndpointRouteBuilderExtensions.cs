using MediatR;

namespace CMS.Auth.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static void MapPostEndpoint<TRequest, TResponse>(this WebApplication app, string endpoint, string? rateLimitPolicyName = null) where TRequest : IRequest<TResponse>
    {
        var routeHandler = app.MapPost(endpoint, async (TRequest request, IMediator mediator) =>
        {
            var response = await mediator.Send(request);
            return Results.Json(response);
        });

        if (rateLimitPolicyName != null)
            routeHandler.RequireRateLimiting(rateLimitPolicyName);
    }
}
