﻿using Calabonga.AspNetCore.AppDefinitions;
using Durak.Game.Web.Definitions.Identity;
using Durak.Game.Web.Endpoints.ProfileEndpoints.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Durak.Game.Web.Endpoints.ProfileEndpoints
{
    public class ProfilesEndpointDefinition : AppDefinition
    {
        public override void ConfigureApplication(WebApplication app)
            => app.MapGet("/api/profiles/get-roles", GetRoles);

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(AuthenticationSchemes = AuthData.AuthSchemes, Policy = "EventItems:UserRoles:View")]
        [FeatureGroupName("Profiles")]
        private async Task<string> GetRoles([FromServices] IMediator mediator, HttpContext context)
            => await mediator.Send(new GetRolesRequest(), context.RequestAborted);
    }
}