namespace CMS.Auth.Tenant;

public interface ITenantAccessor
{
    string TenantId { get; set; }
}
