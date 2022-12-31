using Calabonga.OperationResults;
using Durak.Game.Domain;
using Durak.Game.Web.Endpoints.RoundsEndpoints.ViewModels;

namespace Durak.Game.Web.Endpoints.RoundsEndpoints.Services;

public interface IRoundService
{
    Task<OperationResult<RoundViewModel>> NextRoundAsync(Round round, CancellationToken cancellationToken);
}