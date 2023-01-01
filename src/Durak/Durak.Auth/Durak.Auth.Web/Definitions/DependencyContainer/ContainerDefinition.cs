using Calabonga.AspNetCore.AppDefinitions;
using Durak.Auth.Web.Application.Services;
using Durak.Auth.Web.Definitions.Identity;

namespace Durak.Auth.Web.Definitions.DependencyContainer
{
    /// <summary>
    /// Dependency container definition
    /// </summary>
    public class ContainerDefinition : AppDefinition
    {
        public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ApplicationUserClaimsPrincipalFactory>();
        }
    }
}