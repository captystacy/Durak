using Calabonga.OperationResults;
using Durak.Game.Web.Endpoints.MatchesEndpoints.ViewModels;
using Durak.Game.Web.Endpoints.RoundsEndpoints.ViewModels;

namespace Durak.Game.Web.Endpoints.MatchesEndpoints.Services;

public interface IMatchService
{
    Task<OperationResult<RoundViewModel>> StartAsync(MatchCreateViewModel model, CancellationToken cancellationToken);
}