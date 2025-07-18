using CMS.Application.Abstractions.Services;
using MediatR;

namespace CMS.Application.Features.Commands.Register;
public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly IAuthService _authService;

    public RegisterHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _authService.RegisterAsync(request.Username, request.Email, request.Password, request.Role, request.TenantName, request.Domain);
    }
}
