using MediatR;

namespace CMS.Auth.Features.GetUser;

public record GetUserCommand(string TenantId) : IRequest<GetUserResponse>;
