using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.OperationResults;
using Durak.Game.Web.Endpoints.RoundsEndpoints.Queries;
using Durak.Game.Web.Endpoints.RoundsEndpoints.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Durak.Game.Web.Endpoints.RoundsEndpoints
{
    public class RoundEndpoints : AppDefinition
    {
        public override void ConfigureApplication(WebApplication app)
        {
            app.MapPost("/api/rounds/", PostRound);
            app.MapPut("/api/rounds/{id}", PutRound);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [FeatureGroupName("Rounds")]
        private async Task<OperationResult<RoundViewModel>> PostRound([FromServices] IMediator mediator, RoundCreateViewModel model, HttpContext context)
            => await mediator.Send(new PostRoundRequest(model), context.RequestAborted);

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [FeatureGroupName("Rounds")]
        private async Task<OperationResult<RoundViewModel>> PutRound([FromServices] IMediator mediator, Guid id, RoundUpdateViewModel model, HttpContext context)
            => await mediator.Send(new PutRoundRequest(id, model), context.RequestAborted);

    }
}
