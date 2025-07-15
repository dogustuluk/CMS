namespace CMS.Auth.Features.Login;

public record LoginResponse(bool Status, string Message, string? AccessToken = null, string? RefreshToken = null)
{
}
