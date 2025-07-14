using MediatR;

namespace CMS.Auth.Features.Register;

public record RegisterUserCommand(string Username, string Email, string Password) : IRequest<RegisterUserResponse>;
