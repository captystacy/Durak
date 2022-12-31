using Durak.Game.Web.Endpoints.MovesEndpoints.ViewModels;

namespace Durak.Game.Web.Endpoints.RoundsEndpoints.ViewModels;

public class RoundUpdateViewModel
{
    public Guid Id { get; set; }
    public List<MoveCreateViewModel> Moves { get; set; } = null!;
}