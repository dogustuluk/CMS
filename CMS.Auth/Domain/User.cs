namespace CMS.Auth.Domain;

public class User
{
    public Guid Id { get; set; }
    public string TenantId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiresAt { get; set; }
}
