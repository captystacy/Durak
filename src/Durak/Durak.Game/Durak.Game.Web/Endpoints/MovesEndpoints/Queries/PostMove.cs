using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Durak.Game.Web.Endpoints.MovesEndpoints.Services;
using Durak.Game.Web.Endpoints.MovesEndpoints.ViewModels;
using Durak.Game.Web.Endpoints.RoundsEndpoints.Services;
using Durak.Game.Web.Endpoints.RoundsEndpoints.ViewModels;
using MediatR;

namespace Durak.Game.Web.Endpoints.MovesEndpoints.Queries
{
    public record PostMoveRequest(MoveCreateViewModel Model) : IRequest<OperationResult<RoundViewModel>>;

    public class PostMoveRequestHandler : IRequestHandler<PostMoveRequest, OperationResult<RoundViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMoveService _moveService;
        private readonly IRoundService _roundService;

        public PostMoveRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IMoveService moveService, IRoundService roundService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _moveService = moveService;
            _roundService = roundService;
        }

        public async Task<OperationResult<RoundViewModel>> Handle(PostMoveRequest request, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<RoundViewModel>();

            var insertOperation = await _moveService.InsertAsync(request.Model, cancellationToken);

            if (!insertOperation.Ok || insertOperation.Result is null)
            {
                operation.AddError(insertOperation.GetMetadataMessages());
                return operation;
            }

            var round = insertOperation.Result;

            if (!request.Model.IsLastMoveInRound)
            {
                operation.Result = _mapper.Map<RoundViewModel>(round);
            }

            if (request.Model.IsLastMoveInRound)
            {
                var nextRoundOperation = await _roundService.NextRoundAsync(round!, cancellationToken);

                if (!nextRoundOperation.Ok)
                {
                    operation.AddError(nextRoundOperation.GetMetadataMessages());
                    return operation;
                }

                operation.Result = nextRoundOperation.Result;
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

            return operation;
        }
    }
}
