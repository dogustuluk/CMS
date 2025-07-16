namespace CMS.Auth.Features.Login;

public record AuthenticateUserResponse(bool Status, string Message, string? AccessToken = null, string? RefreshToken = null)
{
}
