using Durak.Game.Domain;
using Durak.Game.Web.Endpoints.CardsEndpoints.ViewModels;

namespace Durak.Game.Web.Endpoints.PlayersEndpoints;

public class PlayerUpdateViewModel
{
    public Guid Id { get; set; }
    public Guid MatchId { get; set; }
    public List<CardViewModel> Cards { get; set; } = new();
    public PlayerRole Role { get; set; }
    public bool IsNearestToDefender { get; set; }
}