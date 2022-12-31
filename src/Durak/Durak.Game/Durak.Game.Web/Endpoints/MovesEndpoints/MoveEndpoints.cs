using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.OperationResults;
using Durak.Game.Web.Endpoints.MovesEndpoints.Queries;
using Durak.Game.Web.Endpoints.MovesEndpoints.ViewModels;
using Durak.Game.Web.Endpoints.RoundsEndpoints.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Durak.Game.Web.Endpoints.MovesEndpoints
{
    public class MoveEndpoints : AppDefinition
    {
        public override void ConfigureApplication(WebApplication app)
        {
            app.MapPost("/api/moves/", PostMove);
        }

        [ProducesResponseType(200)]
        [FeatureGroupName("Moves")]
        private async Task<OperationResult<RoundViewModel>> PostMove([FromServices] IMediator mediator, MoveCreateViewModel model, HttpContext context)
            => await mediator.Send(new PostMoveRequest(model), context.RequestAborted);
    }
}
