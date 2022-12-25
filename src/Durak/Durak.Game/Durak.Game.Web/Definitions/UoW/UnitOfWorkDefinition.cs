using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.UnitOfWork;
using Durak.Game.Infrastructure;

namespace Durak.Game.Web.Definitions.UoW
{
    /// <summary>
    /// Unit of Work registration as MicroserviceDefinition
    /// </summary>
    public class UnitOfWorkDefinition : AppDefinition
    {
        /// <summary>
        /// Configure services for current microservice
        /// </summary>
        /// <param name="services"></param>
        /// <param name="builder"></param>
        public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
            => services.AddUnitOfWork<ApplicationDbContext>();
    }
}