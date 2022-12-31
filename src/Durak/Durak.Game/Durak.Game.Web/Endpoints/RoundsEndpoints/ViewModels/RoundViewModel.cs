using Durak.Game.Web.Endpoints.CardsEndpoints.ViewModels;
using Durak.Game.Web.Endpoints.MatchesEndpoints.ViewModels;

namespace Durak.Game.Web.Endpoints.RoundsEndpoints.ViewModels
{
    public class RoundViewModel
    {
        public Guid Id { get; set; }
        public MatchViewModel Match { get; set; } = null!;
        public Guid MatchId { get; set; }
        public List<CardViewModel> Cards { get; set; } = null!;
    }
}
