using MediatR;

namespace CMS.Auth.Features.Login;

public record LoginCommand(string Email, string Password) : IRequest<LoginResponse>
{

}
