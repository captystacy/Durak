using AutoMapper;
using Durak.Game.Domain;
using Durak.Game.Web.Endpoints.MatchesEndpoints.ViewModels;

namespace Durak.Game.Web.Endpoints.MatchesEndpoints
{
    public class MatchMapperConfiguration : Profile
    {
        public MatchMapperConfiguration()
        {
            CreateMap<Match, MatchViewModel>();

            CreateMap<MatchCreateViewModel, Match>()
                .ForCtorParam("players", x => x.MapFrom(x => x.Players))
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.Deck, x => x.Ignore())
                .ForMember(x => x.TrumpSuit, x => x.Ignore())
                .ForMember(x => x.Rounds, x => x.Ignore())
                .ForMember(x => x.CreatedAt, x => x.Ignore())
                .ForMember(x => x.UpdatedAt, x => x.Ignore())
                .ForMember(x => x.CreatedBy, x => x.Ignore())
                .ForMember(x => x.UpdatedBy, x => x.Ignore());
        }
    }
}
