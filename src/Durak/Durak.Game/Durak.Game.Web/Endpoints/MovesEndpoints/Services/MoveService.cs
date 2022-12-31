using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Durak.Game.Domain;
using Durak.Game.Web.Endpoints.MovesEndpoints.ViewModels;
using Durak.Game.Web.Endpoints.RoundsEndpoints.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Durak.Game.Web.Endpoints.MovesEndpoints.Services
{
    public class MoveService : IMoveService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MoveService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<Round>> InsertAsync(MoveCreateViewModel model, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<Round>();

            var move = _mapper.Map<Move>(model);

            var player = await _unitOfWork.GetRepository<Player>().GetFirstOrDefaultAsync(
                predicate: x => x.Id == model.PlayerId,
                include: x => x.Include(x => x.Cards),
            disableTracking: false);

            if (player is null)
            {
                operation.AddError("Player was not found");
                return operation;
            }

            move.Player = player;

            var round = await _unitOfWork.GetRepository<Round>().GetFirstOrDefaultAsync(
                predicate: x => x.Id == model.RoundId,
                include: x => x
                    .Include(x => x.Match.Players)
                    .Include(x => x.Match.Deck)
                    .Include(x => x.Moves)
                    .Include(x => x.Cards),
                disableTracking: false);

            if (round is null)
            {
                operation.AddError("Round was not found");
                return operation;
            }

            move.Round = round;

            if (model.CardId is not null)
            {
                var card = await _unitOfWork.GetRepository<Card>().GetFirstOrDefaultAsync(
                predicate: x => x.Id == model.CardId && x.PlayerId == model.PlayerId,
                    disableTracking: false);

                if (card is null)
                {
                    operation.AddError("Card was not found or not in hand");
                    return operation;
                }

                move.Card = card;
            }

            var addMoveOperation = round.AddMove(move);

            if (!addMoveOperation.Ok)
            {
                operation.AddError(addMoveOperation.GetMetadataMessages());
                return operation;
            }

            await _unitOfWork.GetRepository<Move>().InsertAsync(move, cancellationToken);

            operation.Result = round;

            return operation;
        }
    }
}
