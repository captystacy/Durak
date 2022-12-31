using Durak.Game.Domain;

namespace Durak.Game.Web.Endpoints.MovesEndpoints.ViewModels;

public class MoveCreateViewModel
{
    public Guid RoundId { get; set; }
    public Guid PlayerId { get; set; }
    public Guid? CardId { get; set; }
    public MoveType Type { get; set; }
    public bool IsLastMoveInRound { get; set; }
}