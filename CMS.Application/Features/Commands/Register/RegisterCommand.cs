using MediatR;

namespace CMS.Application.Features.Commands.Register;
public record RegisterCommand(string Username, string Email, string Password, int Role, string TenantName, string Domain) : IRequest<RegisterResponse>;