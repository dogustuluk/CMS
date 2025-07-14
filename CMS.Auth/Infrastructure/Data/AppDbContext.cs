using CMS.Auth.Domain;
using CMS.Auth.Tenant;
using Microsoft.EntityFrameworkCore;

namespace CMS.Auth.Infrastructure.Data;

public class AppDbContext : DbContext
{
    private readonly ITenantAccessor _tenantAccessor;

    public AppDbContext(DbContextOptions<AppDbContext> options, ITenantAccessor tenantAccessor) : base(options)
    {
        _tenantAccessor = tenantAccessor;
    }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasQueryFilter(a => a.TenantId == _tenantAccessor.TenantId);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var item in ChangeTracker.Entries<User>().Where(a => a.State == EntityState.Added))
        {
            item.Entity.TenantId = _tenantAccessor.TenantId;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
