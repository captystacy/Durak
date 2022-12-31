using Microsoft.EntityFrameworkCore;

namespace Durak.Game.Infrastructure.DatabaseInitialization
{
    /// <summary>
    /// Database Initializer
    /// </summary>
    public static class DatabaseInitializer
    {
        public static async void Seed(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            await using var context = scope.ServiceProvider.GetService<ApplicationDbContext>()!;

            // It should be uncomment when using UseSqlServer() settings or any other providers.
            // This is should not be used when UseInMemoryDatabase()
            // await context!.Database.MigrateAsync();

            if (!await context.EventItems.AnyAsync(x => x.Id == Guid.Parse("1467a5b9-e61f-82b0-425b-7ec75f5c5029")))
            {
                await context.EventItems.AddAsync(new EventItem
                {
                    CreatedAt = DateTime.UtcNow,
                    Id = Guid.Parse("1467a5b9-e61f-82b0-425b-7ec75f5c5029"),
                    Level = "Information",
                    Logger = "SEED",
                    Message = "Seed method some entities successfully save to ApplicationDbContext"
                });
            }

            var players = new List<Player>
            {
                new()
                {
                    Id = Guid.Parse("89a1c625-d947-4563-82a2-453542788139"),
                },
                new()
                {
                    Id = Guid.Parse("4a9408f3-80f9-4083-b1f1-7c1ce7131da1"),
                },
            };

            var match = new Match(players)
            {
                CreatedAt = DateTime.UtcNow,
                Id = Guid.Parse("4a3db619-3d47-4bb3-9a46-9a5a3f72e7ea"),
            };

            var round = match.Start();

            round.Id = Guid.Parse("82879795-6c8f-43f6-8637-857581caabac");

            if (!await context.Matches.AnyAsync(x => x.Id == Guid.Parse("4a3db619-3d47-4bb3-9a46-9a5a3f72e7ea")))
            {
                await context.Matches.AddAsync(match);
            }

            match.Deal();

            await context.SaveChangesAsync();
        }
    }
}