namespace Durak.Game.Domain;

public class Card : Identity
{
    public Round? Round { get; set; } = null!;
    public Guid? RoundId { get; set; }
    public Match Match { get; set; } = null!;
    public Guid MatchId { get; set; }
    public Player? Player { get; set; }
    public Guid? PlayerId { get; set; }
    public Move? Move { get; set; }
    public Suit Suit { get; }
    public int Value { get; }

    public Card(int value, Suit suit)
    {
        Value = value;
        Suit = suit;
    }

    protected bool Equals(Card other)
    {
        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Card)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Suit, Value);
    }
}

public enum Suit
{
    None = 0,
    Club = 1,
    Diamond = 2,
    Heart = 3,
    Spade = 4,
}