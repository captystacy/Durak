using Calabonga.OperationResults;

namespace Durak.Game.Domain;

public class Move : Identity
{
    public Round Round { get; set; } = null!;
    public Guid RoundId { get; set; }
    public Player Player { get; set; } = null!;
    public Guid PlayerId { get; set; }
    public Card? Card { get; set; }
    public Guid? CardId { get; set; }
    public MoveType Type { get; set; }

    private OperationResult<Empty> PutDown()
    {
        var operation = OperationResult.CreateResult<Empty>();

        if (Card is null)
        {
            operation.AddError("Can't put down null card");
            return operation;
        }

        var isCardWithTheSameValueOnTheTable = Round.Cards.Exists(x => x.Value == Card.Value);

        switch (Player.Role)
        {
            case PlayerRole.Thrower:

                if (!Round.Cards.Any())
                {
                    operation.AddError("Thrower can't throw cards because table was empty");
                    return operation;
                }

                if (Player.IsNearestToDefender && !isCardWithTheSameValueOnTheTable)
                {
                    operation.AddError("Nearest thrower tried to put down card with value that was not on the table");
                    return operation;
                }

                if (!Player.IsNearestToDefender && Card.Value != Round.Cards.First().Value)
                {
                    operation.AddError("Not nearest to defender thrower tried to put down not-first-card value");
                    return operation;
                }

                break;
            case PlayerRole.Attacker:

                if (Round.Cards.Any() && !isCardWithTheSameValueOnTheTable)
                {
                    operation.AddError("Attacker tried to put down card with value that was not on the table");
                    return operation;
                }

                break;
            case PlayerRole.Defender:

                if (!Round.Cards.Any())
                {
                    operation.AddError("Defender can't beat cards because table was empty");
                    return operation;
                }

                var lastCard = Round.Cards.Last();

                if (lastCard.Suit == Card.Suit && lastCard.Value > Card.Value)
                {
                    operation.AddError("Defender tried to beat card which value is less than last on the table");
                    return operation;
                }

                var trumpSuit = Round.Match.TrumpSuit;

                if (lastCard.Suit == trumpSuit && Card.Suit != trumpSuit)
                {
                    operation.AddError("Defender tried to beat a trump card with a default card");
                    return operation;
                }

                break;
            case PlayerRole.None:
            default:
                operation.AddError("Player role was not implemented");
                break;
        }

        Round.Cards.Add(Card);

        Player.Cards.Remove(Card);

        return operation;
    }

    private OperationResult<Empty> PickUp()
    {
        var operation = OperationResult.CreateResult<Empty>();

        switch (Player.Role)
        {
            case PlayerRole.Defender:
                Player.Cards.AddRange(Round.Cards);

                Round.Cards.Clear();

                Round.ThrowersCanThrow = true;
                break;
            case PlayerRole.Thrower:
            case PlayerRole.Attacker:
                operation.AddError("Thrower and attacker can't pick up cards");
                break;
            case PlayerRole.None:
            default:
                operation.AddError("Player role was not implemented");
                break;
        }

        return operation;
    }

    public OperationResult<Empty> Pass()
    {
        var operation = OperationResult.CreateResult<Empty>();

        switch (Player.Role)
        {
            case PlayerRole.Attacker:
                Round.ThrowersCanThrow = true;
                break;
            case PlayerRole.Defender:
                operation.AddError("Defender can't pass");
                break;
            case PlayerRole.Thrower:
                break;
            case PlayerRole.None:
            default:
                operation.AddError("Player role was not implemented");
                break;
        }

        return operation;
    }

    public OperationResult<Move> Execute()
    {
        var operation = OperationResult.CreateResult<Move>();

        switch (Type)
        {
            case MoveType.PutDown:
                var putDownOperation = PutDown();

                if (!putDownOperation.Ok)
                {
                    operation.AddError(putDownOperation.GetMetadataMessages());
                }

                break;
            case MoveType.PickUp:
                var pickUpOperation = PickUp();

                if (!pickUpOperation.Ok)
                {
                    operation.AddError(pickUpOperation.GetMetadataMessages());
                }

                break;
            case MoveType.Pass:
                var passOperation = Pass();

                if (!passOperation.Ok)
                {
                    operation.AddError(passOperation.GetMetadataMessages());
                }

                break;
            case MoveType.None:
            default:
                operation.AddError("Move type was not implemented");
                break;
        }

        operation.Result = this;

        return operation;
    }
}

public enum MoveType
{
    None = 0,
    PutDown = 1,
    PickUp = 2,
    Pass = 3,
}