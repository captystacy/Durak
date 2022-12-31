using AutoMapper;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Durak.Game.Domain;
using Durak.Game.Web.Endpoints.PlayersEndpoints.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Durak.Game.Web.Endpoints.PlayersEndpoints.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IRepository<Player> _repository;
        private readonly IMapper _mapper;

        public PlayerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = unitOfWork.GetRepository<Player>();
            _mapper = mapper;
        }

        public async Task<OperationResult<PlayerViewModel>> UpdateAsync(PlayerUpdateViewModel model)
        {
            var operation = OperationResult.CreateResult<PlayerViewModel>();

            var player = await _repository.GetFirstOrDefaultAsync(
                predicate: x => x.Id == model.Id,
                include: x => x.Include(x => x.Cards));

            if (player is null)
            {
                operation.AddError("Player was not found");
                return operation;
            }

            _mapper.Map(model, player);

            _repository.Update(player);

            operation.Result = _mapper.Map<PlayerViewModel>(player);

            return operation;
        }
    }
}
