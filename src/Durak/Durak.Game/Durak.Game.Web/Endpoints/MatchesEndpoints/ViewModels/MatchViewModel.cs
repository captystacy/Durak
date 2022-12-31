using Durak.Game.Domain;
using Durak.Game.Web.Endpoints.PlayersEndpoints.ViewModels;

namespace Durak.Game.Web.Endpoints.MatchesEndpoints.ViewModels
{
    public class MatchViewModel
    {
        public Guid Id { get; set; }
        public Suit TrumpSuit { get; set; }
        public List<PlayerViewModel> Players { get; set; } = null!;
    }
}
