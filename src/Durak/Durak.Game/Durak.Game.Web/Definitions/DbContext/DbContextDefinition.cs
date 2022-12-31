using Calabonga.AspNetCore.AppDefinitions;
using Durak.Game.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Durak.Game.Web.Definitions.DbContext
{
    /// <summary>
    /// ASP.NET Core services registration and configurations
    /// </summary>
    public class DbContextDefinition : AppDefinition
    {
        /// <summary>
        /// Configure services for current microservice
        /// </summary>
        /// <param name="services"></param>
        /// <param name="builder"></param>
        public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
            => services.AddDbContext<ApplicationDbContext>(config =>
            {
                config.UseSqlServer(builder.Configuration.GetConnectionString(nameof(ApplicationDbContext)));
            });
    }
}