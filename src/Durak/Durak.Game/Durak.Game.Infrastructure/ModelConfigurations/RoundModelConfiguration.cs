using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Durak.Game.Infrastructure.ModelConfigurations
{
    public class RoundModelConfiguration : IEntityTypeConfiguration<Round>
    {
        public void Configure(EntityTypeBuilder<Round> builder)
        {
            builder.ToTable("Rounds");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);
            builder.Property(x => x.ThrowersCanThrow);

            builder
                .HasMany(x => x.Moves)
                .WithOne(x => x.Round)
                .HasForeignKey(x => x.RoundId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.Cards)
                .WithOne(x => x.Round)
                .HasForeignKey(x => x.RoundId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
