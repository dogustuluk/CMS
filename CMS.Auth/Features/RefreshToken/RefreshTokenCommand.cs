using MediatR;

namespace CMS.Auth.Features.RefreshToken;

public record RefreshTokenCommand(string RefreshToken, Guid UserId) : IRequest<RefreshTokenResponse>
{
}
