using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Durak.Game.Infrastructure.ModelConfigurations
{
    public class PlayerModelConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("Players");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);
            builder.Property(x => x.IsNearestToDefender);

            builder
                .HasMany(x => x.Cards)
                .WithOne(x => x.Player)
                .HasForeignKey(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.Moves)
                .WithOne(x => x.Player)
                .HasForeignKey(x => x.PlayerId);
        }
    }
}

