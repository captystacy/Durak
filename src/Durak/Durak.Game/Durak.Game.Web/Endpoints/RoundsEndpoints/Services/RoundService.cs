using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Durak.Game.Domain;
using Durak.Game.Web.Endpoints.RoundsEndpoints.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Durak.Game.Web.Endpoints.RoundsEndpoints.Services
{
    public class RoundService : IRoundService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoundService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<RoundViewModel>> NextRoundAsync(Round round, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<RoundViewModel>();

            var nextRound = round.NextRound();

            await _unitOfWork.GetRepository<Round>().InsertAsync(nextRound, cancellationToken);

            operation.Result = _mapper.Map<RoundViewModel>(nextRound);

            return operation;
        }
    }
}
