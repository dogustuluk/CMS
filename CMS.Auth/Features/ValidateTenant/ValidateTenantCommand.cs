using MediatR;

namespace CMS.Auth.Features.ValidateTenant;

public record ValidateTenantCommand(string TenantId) : IRequest<ValidateTenantResponse>;
