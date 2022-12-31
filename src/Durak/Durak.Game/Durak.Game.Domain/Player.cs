namespace Durak.Game.Domain;

public class Player : Identity
{
    public Match Match { get; set; } = null!;
    public Guid MatchId { get; set; }
    public PlayerRole Role { get; set; }
    public bool IsNearestToDefender { get; set; }
    public List<Card> Cards { get; set; } = new();
    public List<Move> Moves { get; set; } = null!;

    public Card? GetLowestCard(Suit suit)
    {
        return Cards.Where(x => x.Suit == suit).MinBy(x => x.Value);
    }

    public bool IsPlaying()
    {
        return Cards.Count > 0;
    }

    public void ClearRoles()
    {
        Role = PlayerRole.Thrower;
        IsNearestToDefender = false;
    }
}

public enum PlayerRole
{
    None = 0,
    Thrower = 1,
    Attacker = 2,
    Defender = 3,
} 