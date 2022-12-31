using Calabonga.OperationResults;
using Durak.Game.Domain;
using Durak.Game.Web.Endpoints.MovesEndpoints.ViewModels;

namespace Durak.Game.Web.Endpoints.MovesEndpoints.Services;

public interface IMoveService
{
    Task<OperationResult<Round>> InsertAsync(MoveCreateViewModel model, CancellationToken cancellationToken);
}