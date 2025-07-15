using CMS.Auth.Domain;
using Microsoft.EntityFrameworkCore;

namespace CMS.Auth.Infrastructure.Data;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<User> Users => Set<User>();
    public DbSet<CMS.Auth.Domain.Tenant> Tenants => Set<CMS.Auth.Domain.Tenant>();

}
