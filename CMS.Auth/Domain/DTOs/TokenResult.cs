namespace CMS.Auth.Domain.DTOs;

public class TokenResult
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpiresAt { get; set; }
}
