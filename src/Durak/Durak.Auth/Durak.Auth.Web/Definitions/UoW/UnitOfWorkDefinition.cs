using Calabonga.AspNetCore.AppDefinitions;
using Calabonga.UnitOfWork;
using Durak.Auth.Infrastructure;

namespace Durak.Auth.Web.Definitions.UoW
{
    /// <summary>
    /// Unit of Work registration as application definition
    /// </summary>
    public class UnitOfWorkDefinition : AppDefinition
    {
        /// <summary>
        /// Configure services for current application
        /// </summary>
        /// <param name="services"></param>
        /// <param name="builder"></param>
        public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
            => services.AddUnitOfWork<ApplicationDbContext>();
    }
}