﻿using Calabonga.AspNetCore.AppDefinitions;
using Durak.Game.Web.Application;
using MediatR;
using System.Reflection;

namespace Durak.Game.Web.Definitions.Mediator
{
    /// <summary>
    /// Register Mediator as MicroserviceDefinition
    /// </summary>
    public class MediatorDefinition : AppDefinition
    {
        /// <summary>
        /// Configure services for current microservice
        /// </summary>
        /// <param name="services"></param>
        /// <param name="builder"></param>
        public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}