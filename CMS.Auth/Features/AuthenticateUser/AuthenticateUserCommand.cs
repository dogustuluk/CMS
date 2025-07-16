using MediatR;

namespace CMS.Auth.Features.Login;

public record AuthenticateUserCommand(string Email, string Password) : IRequest<AuthenticateUserResponse>
{

}
