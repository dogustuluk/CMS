namespace CMS.Auth.Features.GetUser;

public record GetUserResponse(bool Status, Guid? UserId = null, string? UserName = null, string? Email = null, string? TenantId = null, string? Domain = null, string? TenantName = null);