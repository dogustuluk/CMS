namespace CMS.Auth.Infrastructure.Configurations;

public class JWTSettings
{
    public string SecretKey { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int ExpiryMinutes { get; set; } = 120;
    public int RefreshTokenDays { get; set; } = 7;
}


