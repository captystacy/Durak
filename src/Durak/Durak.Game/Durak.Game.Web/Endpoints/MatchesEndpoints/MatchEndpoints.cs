using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.OperationResults;
using Durak.Game.Web.Endpoints.MatchesEndpoints.Queries;
using Durak.Game.Web.Endpoints.MatchesEndpoints.ViewModels;
using Durak.Game.Web.Endpoints.RoundsEndpoints.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Durak.Game.Web.Endpoints.MatchesEndpoints
{
    public class MatchEndpoints : AppDefinition
    {
        public override void ConfigureApplication(WebApplication app)
        {
            app.MapPost("/api/matches/", PostMatch);
        }

        [ProducesResponseType(200)]
        [FeatureGroupName("Matches")]
        private async Task<OperationResult<RoundViewModel>> PostMatch([FromServices] IMediator mediator, MatchCreateViewModel model, HttpContext context)
            => await mediator.Send(new PostMatchRequest(model), context.RequestAborted);
    }
}
