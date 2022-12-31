using Durak.Game.Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Durak.Game.Infrastructure
{
    /// <summary>
    /// Database context for current application
    /// </summary>
    public class ApplicationDbContext : DbContextBase
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Round> Rounds { get; set; }

        public DbSet<Move> Moves { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<EventItem> EventItems { get; set; }

        public DbSet<ApplicationUserProfile> Profiles { get; set; }

        public DbSet<MicroservicePermission> Permissions { get; set; }
    }
}