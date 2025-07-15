using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CMS.Auth.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        //AppDbContextFactory, EF Core migration işlemleri sırasında DbContext'i dependency injection (örneğin ITenantAccessor) olmadan manuel olarak oluşturabilmek için yazdım.
        // Buradaki connection string sadece migration içindir!
        // Config'den okunmaz çünkü DI aktif değildir.
        optionsBuilder.UseNpgsql("User ID=postgres;Password=7575;Host=localhost;Port=5432;Database=CMSAuthDB;");

        // tenantAccessor = null => çünkü design-time’da middleware yok
        return new AppDbContext(optionsBuilder.Options);
    }
}
