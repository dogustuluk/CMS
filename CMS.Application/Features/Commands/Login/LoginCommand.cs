using MediatR;

namespace CMS.Application.Features.Commands.Login;
public record LoginCommand(string Email, string Password) : IRequest<LoginResponse>;
