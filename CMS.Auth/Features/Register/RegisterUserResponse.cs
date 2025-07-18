namespace CMS.Auth.Features.Register;

public record RegisterUserResponse(bool Success, string? ErrorMessage = null, Guid? Id = null);

