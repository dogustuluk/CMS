namespace CMS.Application.Features.Commands.Login;
public record LoginResponse(bool Status, string? AccessToken = null, string? RefreshToken = null, string? Message = null);
