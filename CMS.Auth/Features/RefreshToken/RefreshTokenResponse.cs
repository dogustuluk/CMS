namespace CMS.Auth.Features.RefreshToken;

public record RefreshTokenResponse(bool Status, string Message, string? AccessToken = null, Guid? UserId = null);