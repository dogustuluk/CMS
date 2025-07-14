namespace CMS.Auth.Tenant;

public class TenantAccessor : ITenantAccessor
{
    public string TenantId { get; set; } = string.Empty;
}
