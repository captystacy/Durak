using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Durak.Game.Web.Endpoints.MatchesEndpoints.Services;
using Durak.Game.Web.Endpoints.MatchesEndpoints.ViewModels;
using Durak.Game.Web.Endpoints.RoundsEndpoints.ViewModels;
using MediatR;

namespace Durak.Game.Web.Endpoints.MatchesEndpoints.Queries
{
    public record PostMatchRequest(MatchCreateViewModel Model) : IRequest<OperationResult<RoundViewModel>>;

    public class PostMatchRequestHandler : IRequestHandler<PostMatchRequest, OperationResult<RoundViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMatchService _matchService;


        public PostMatchRequestHandler(IUnitOfWork unitOfWork, IMatchService matchService)
        {
            _unitOfWork = unitOfWork;
            _matchService = matchService;
        }

        public async Task<OperationResult<RoundViewModel>> Handle(PostMatchRequest request, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<RoundViewModel>();

            var startOperation = await _matchService.StartAsync(request.Model, cancellationToken);

            if (!startOperation.Ok)
            {
                operation.AddError(startOperation.GetMetadataMessages());
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

            operation.Result = startOperation.Result;

            return operation;
        }
    }
}
