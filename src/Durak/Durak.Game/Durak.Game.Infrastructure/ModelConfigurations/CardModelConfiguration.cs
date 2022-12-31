using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Durak.Game.Infrastructure.ModelConfigurations
{
    public class CardModelConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable("Cards");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);
            builder.Property(x => x.Value);
            builder.Property(x => x.Suit);
            builder.Property(x => x.RoundId);
            builder.Property(x => x.MatchId);
            builder.Property(x => x.PlayerId);
        }
    }
}
