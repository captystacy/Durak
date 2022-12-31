using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Durak.Game.Infrastructure.ModelConfigurations
{
    public class MoveModelConfiguration : IEntityTypeConfiguration<Move>
    {
        public void Configure(EntityTypeBuilder<Move> builder)
        {
            builder.ToTable("Moves");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);
            builder.Property(x => x.Type);
            builder.Property(x => x.CardId);

            builder.HasOne(x => x.Card)
                .WithOne(x => x.Move)
                .HasForeignKey<Move>(x => x.CardId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
