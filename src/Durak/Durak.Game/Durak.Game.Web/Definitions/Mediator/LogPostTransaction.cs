using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Durak.Game.Web.Definitions.Mediator.Base;
using Durak.Game.Web.Endpoints.EventItemsEndpoints.ViewModels;
using MediatR;

namespace Durak.Game.Web.Definitions.Mediator
{
    public class LogPostTransactionBehavior : TransactionBehavior<IRequest<OperationResult<EventItemViewModel>>, OperationResult<EventItemViewModel>>
    {
        public LogPostTransactionBehavior(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}