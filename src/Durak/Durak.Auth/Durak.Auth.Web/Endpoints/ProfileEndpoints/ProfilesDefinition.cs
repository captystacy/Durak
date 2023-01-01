using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.OperationResults;
using Durak.Auth.Web.Application;
using Durak.Auth.Web.Definitions.OpenIddict;
using Durak.Auth.Web.Endpoints.ProfileEndpoints.Queries;
using Durak.Auth.Web.Endpoints.ProfileEndpoints.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Durak.Auth.Web.Endpoints.ProfileEndpoints
{
    public class ProfilesDefinition : AppDefinition
    {
        public override void ConfigureApplication(WebApplication app)
        {
            app.MapGet("/api/profiles/get-roles", GetRoles);
            app.MapPost("/api/profiles/register", RegisterAccount);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(AuthenticationSchemes = AuthData.AuthSchemes)]
        [FeatureGroupName("Profiles")]
        private async Task<string> GetRoles([FromServices] IMediator mediator, HttpContext context)
            => await mediator.Send(new GetRolesRequest(), context.RequestAborted);

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [FeatureGroupName("Profiles")]
        private async Task<OperationResult<UserProfileViewModel>> RegisterAccount([FromServices] IMediator mediator, RegisterViewModel model, HttpContext context)
            => await mediator.Send(new RegisterAccountRequest(model), context.RequestAborted);
    }
}