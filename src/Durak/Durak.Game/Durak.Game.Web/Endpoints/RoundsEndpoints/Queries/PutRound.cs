using AutoMapper;
using Calabonga.Microservices.Core.Exceptions;
using Calabonga.Microservices.Core;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Durak.Game.Domain;
using Durak.Game.Web.Endpoints.RoundsEndpoints.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Durak.Game.Web.Endpoints.RoundsEndpoints.Queries
{
    public record PutRoundRequest(Guid Id, RoundUpdateViewModel Model) : IRequest<OperationResult<RoundViewModel>>;

    public class PutRoundRequestHandler : IRequestHandler<PutRoundRequest, OperationResult<RoundViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PutRoundRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<RoundViewModel>> Handle(PutRoundRequest request, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<RoundViewModel>();

            var repository = _unitOfWork.GetRepository<Round>();

            var round = await repository.GetFirstOrDefaultAsync(
                predicate: x => x.Id == request.Id,
                include: i => i
                    .Include(x => x.Match.Players)
                    .Include(x => x.Match.Deck));

            if (round is null)
            {
                operation.AddError("Round was not found");
                return operation;
            }

            round.Moves = _mapper.Map<List<Move>>(request.Model.Moves);

            repository.Update(round);

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

            operation.Result = _mapper.Map<RoundViewModel>(round);

            return operation;
        }
    }
}
