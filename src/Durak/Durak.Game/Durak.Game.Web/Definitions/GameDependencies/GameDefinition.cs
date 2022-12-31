using Calabonga.AspNetCore.AppDefinitions;
using Durak.Game.Web.Endpoints.MatchesEndpoints.Services;
using Durak.Game.Web.Endpoints.MovesEndpoints.Services;
using Durak.Game.Web.Endpoints.PlayersEndpoints.Services;
using Durak.Game.Web.Endpoints.RoundsEndpoints.Services;

namespace Durak.Game.Web.Definitions.GameDependencies
{
    public class GameDefinition : AppDefinition
    {
        public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IMoveService, MoveService>();
            services.AddScoped<IRoundService, RoundService>();
            services.AddScoped<IMatchService, MatchService>();
        }
    }
}
