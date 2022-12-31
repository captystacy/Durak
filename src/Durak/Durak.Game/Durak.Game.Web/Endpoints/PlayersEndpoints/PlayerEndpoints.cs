using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.OperationResults;
using Durak.Game.Web.Endpoints.PlayersEndpoints.Queries;
using Durak.Game.Web.Endpoints.PlayersEndpoints.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Durak.Game.Web.Endpoints.PlayersEndpoints
{
    public class PlayerEndpoints : AppDefinition
    {
        public override void ConfigureApplication(WebApplication app)
        {
            app.MapPut("/api/players/", PutPlayer);
        }

        [ProducesResponseType(200)]
        [FeatureGroupName("Players")]
        private async Task<OperationResult<PlayerViewModel>> PutPlayer([FromServices] IMediator mediator, PlayerUpdateViewModel model, HttpContext context)
            => await mediator.Send(new PutPlayerRequest(model), context.RequestAborted);
    }
}
