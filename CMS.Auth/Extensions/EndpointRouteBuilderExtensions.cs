using MediatR;

namespace CMS.Auth.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static void MapPostEndpoint<TRequest, TResponse>(this WebApplication app, string endpoint) where TRequest : IRequest<TResponse>
    {
        app.MapPost(endpoint, async (TRequest request, IMediator mediator) =>
        {
            var response = await mediator.Send(request);
            return Results.Json(response);
        });
    }
}
