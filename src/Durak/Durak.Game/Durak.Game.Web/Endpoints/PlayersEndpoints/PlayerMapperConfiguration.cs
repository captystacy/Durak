using AutoMapper;
using Calabonga.UnitOfWork;
using Durak.Game.Domain;
using Durak.Game.Web.Definitions.Mapping;
using Durak.Game.Web.Endpoints.PlayersEndpoints.ViewModels;

namespace Durak.Game.Web.Endpoints.PlayersEndpoints
{
    public class PlayerMapperConfiguration : Profile
    {
        public PlayerMapperConfiguration()
        {
            CreateMap<Player, PlayerViewModel>();

            CreateMap<Player, PlayerUpdateViewModel>();

            CreateMap<PlayerUpdateViewModel, Player>()
                .ForMember(x => x.Moves, x => x.Ignore())
                .ForMember(x => x.Match, x => x.Ignore());

            CreateMap<PlayerViewModel, Player>()
                .ForMember(x => x.IsNearestToDefender, x => x.Ignore())
                .ForMember(x => x.Moves, x => x.Ignore())
                .ForMember(x => x.Role, x => x.Ignore())
                .ForMember(x => x.Match, x => x.Ignore())
                .ForMember(x => x.MatchId, x => x.Ignore())
                .ForMember(x => x.Cards, x => x.Ignore());

            CreateMap<IPagedList<Player>, IPagedList<PlayerViewModel>>()
                .ConvertUsing<PagedListConverter<Player, PlayerViewModel>>();
        }
    }
}
