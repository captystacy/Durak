using Durak.Game.Web.Endpoints.PlayersEndpoints.ViewModels;

namespace Durak.Game.Web.Endpoints.MatchesEndpoints.ViewModels
{
    public class MatchCreateViewModel
    {
        public List<PlayerViewModel> Players { get; set; } = null!;
    }
}
