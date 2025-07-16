namespace CMS.Application.Common.DTOs;
public class TokenResultDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string Message { get; set; }
    public DateTime ExpiresAt { get; set; }
}
