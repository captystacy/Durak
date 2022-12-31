using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Durak.Game.Web.Endpoints.PlayersEndpoints.Services;
using Durak.Game.Web.Endpoints.PlayersEndpoints.ViewModels;
using MediatR;

namespace Durak.Game.Web.Endpoints.PlayersEndpoints.Queries
{
    public record PutPlayerRequest(PlayerUpdateViewModel Model) : IRequest<OperationResult<PlayerViewModel>>;

    public class PutPlayerRequestHandler : IRequestHandler<PutPlayerRequest, OperationResult<PlayerViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPlayerService _playerService;

        public PutPlayerRequestHandler(IUnitOfWork unitOfWork, IPlayerService playerService)
        {
            _unitOfWork = unitOfWork;
            _playerService = playerService;
        }

        public async Task<OperationResult<PlayerViewModel>> Handle(PutPlayerRequest request, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<PlayerViewModel>();

            var updateOperation = await _playerService.UpdateAsync(request.Model);

            if (!updateOperation.Ok)
            {
                operation.AddError(updateOperation.GetMetadataMessages());
                return operation;
            }

            await _unitOfWork.SaveChangesAsync();

            if (!_unitOfWork.LastSaveChangesResult.IsOk)
            {
                operation.AddError(_unitOfWork.LastSaveChangesResult.Exception!.Message);

                if (_unitOfWork.LastSaveChangesResult.Exception.InnerException is not null)
                {
                    operation.AddError(_unitOfWork.LastSaveChangesResult.Exception.InnerException.Message);
                }

                return operation;
            }

            operation.Result = updateOperation.Result;

            return operation;
        }
    }
}
