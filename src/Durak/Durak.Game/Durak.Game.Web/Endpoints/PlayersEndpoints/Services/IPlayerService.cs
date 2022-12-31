using Calabonga.OperationResults;
using Durak.Game.Web.Endpoints.PlayersEndpoints.ViewModels;

namespace Durak.Game.Web.Endpoints.PlayersEndpoints.Services;

public interface IPlayerService
{
    Task<OperationResult<PlayerViewModel>> UpdateAsync(PlayerUpdateViewModel model);
}