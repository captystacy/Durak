using AutoMapper;
using Calabonga.UnitOfWork;
using Durak.Game.Domain;
using Durak.Game.Web.Definitions.Mapping;
using Durak.Game.Web.Endpoints.CardsEndpoints.ViewModels;

namespace Durak.Game.Web.Endpoints.CardsEndpoints
{
    public class CardMapperConfiguration : Profile
    {
        public CardMapperConfiguration()
        {
            CreateMap<Card, CardViewModel>();

            CreateMap<CardViewModel, Card>()
                .ForCtorParam("value", x => x.MapFrom(x => x.Value))
                .ForCtorParam("suit", x => x.MapFrom(x => x.Suit))
                .ForMember(x => x.Move, x => x.Ignore())
                .ForMember(x => x.Match, x => x.Ignore())
                .ForMember(x => x.MatchId, x => x.Ignore())
                .ForMember(x => x.Round, x => x.Ignore())
                .ForMember(x => x.RoundId, x => x.Ignore())
                .ForMember(x => x.Player, x => x.Ignore())
                .ForMember(x => x.PlayerId, x => x.Ignore());

            CreateMap<IPagedList<Card>, IPagedList<CardViewModel>>()
                .ConvertUsing<PagedListConverter<Card, CardViewModel>>();
        }
    }

}
