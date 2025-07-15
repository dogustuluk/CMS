using CMS.Auth.Domain;
using CMS.Auth.Tenant;
using Microsoft.EntityFrameworkCore;

namespace CMS.Auth.Infrastructure.Data;

public class AppDbContext : DbContext
{
    private readonly string? _tenantId;

    public AppDbContext(DbContextOptions<AppDbContext> options, ITenantAccessor? tenantAccessor = null)
        : base(options)
    {
        _tenantId = tenantAccessor?.TenantId;
    }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (!string.IsNullOrEmpty(_tenantId))
        {
            modelBuilder.Entity<User>()
                .HasQueryFilter(u => u.TenantId == _tenantId);
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrEmpty(_tenantId))
        {
            foreach (var item in ChangeTracker.Entries<User>().Where(a => a.State == EntityState.Added))
            {
                item.Entity.TenantId = _tenantId;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
