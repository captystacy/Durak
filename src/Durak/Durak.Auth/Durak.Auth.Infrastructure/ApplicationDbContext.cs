using Durak.Auth.Domain;
using Durak.Auth.Infrastructure;
using Durak.Auth.Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Durak.Auth.Infrastructure
{
    /// <summary>
    /// Database context for current application
    /// </summary>
    public class ApplicationDbContext : DbContextBase
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<EventItem> EventItems { get; set; }

        public DbSet<ApplicationUserProfile> Profiles { get; set; }

        public DbSet<AppPermission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.UseOpenIddict<Guid>();
            base.OnModelCreating(builder);
        }
    }
}

/// <summary>
/// ATTENTION!
/// It should uncomment two line below when using real Database (not in memory mode). Don't forget update connection string.
/// </summary>
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer("Server=192.168.100.7,1433;Database=DurakAuth;User ID=sa;Password=S3cur3P@ssW0rd!;MultipleActiveResultSets=true;TrustServerCertificate=True");
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}