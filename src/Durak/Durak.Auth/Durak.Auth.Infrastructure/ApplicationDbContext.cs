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
        optionsBuilder.UseSqlServer("Server=localhost;Database=DurakAuth;User ID=sa;Password=KWud6MtrL3M3xaUB!QrcnSq9YVhHVT4VtYxe249HY$*fxcLp5p39Hj^&pFLFK7VC;TrustServerCertificate=True");
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}