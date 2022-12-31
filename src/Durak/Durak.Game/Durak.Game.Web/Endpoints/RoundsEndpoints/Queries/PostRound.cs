using AutoMapper;
using Calabonga.Microservices.Core;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Durak.Game.Domain;
using Durak.Game.Web.Endpoints.RoundsEndpoints.ViewModels;
using MediatR;

namespace Durak.Game.Web.Endpoints.RoundsEndpoints.Queries
{
    public record PostRoundRequest(RoundCreateViewModel Model) : IRequest<OperationResult<RoundViewModel>>;

    public class PostRoundRequestHandler : IRequestHandler<PostRoundRequest, OperationResult<RoundViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PostRoundRequestHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<OperationResult<RoundViewModel>> Handle(PostRoundRequest request, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<RoundViewModel>();

            var roundRepository = _unitOfWork.GetRepository<Round>();

            var putRoundOperation = await _mediator.Send(new PutRoundRequest(request.Model.LastRound.Id, request.Model.LastRound), cancellationToken);

            if (!putRoundOperation.Ok || putRoundOperation.Result is null)
            {
                operation.AddError(putRoundOperation.GetMetadataMessages());
                return operation;
            }

            var round = await roundRepository.FindAsync(putRoundOperation.Result.Id);

            if (round is null)
            {
                operation.AddError("Round was not found");
                return operation;
            }

            var nextRound = round.NextRound();

            await roundRepository.InsertAsync(nextRound, cancellationToken);

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

            operation.Result = _mapper.Map<RoundViewModel>(nextRound);

            return operation;
        }
    }
}
