using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Durak.Auth.Web.Definitions.Mediator.Base;
using Durak.Auth.Web.Endpoints.EventItemsEndpoints.ViewModels;
using MediatR;

namespace Durak.Auth.Web.Definitions.Mediator
{
    public class EventItemPostTransactionBehavior : TransactionBehavior<IRequest<OperationResult<EventItemViewModel>>, OperationResult<EventItemViewModel>>
    {
        public EventItemPostTransactionBehavior(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}