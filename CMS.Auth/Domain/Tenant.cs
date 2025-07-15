namespace CMS.Auth.Domain;

public class Tenant
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public string Domain { get; set; }
    public DateTime CreatedAt { get; set; }
}
