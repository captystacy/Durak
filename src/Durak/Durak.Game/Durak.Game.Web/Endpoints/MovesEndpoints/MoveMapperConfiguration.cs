using AutoMapper;
using Calabonga.UnitOfWork;
using Durak.Game.Domain;
using Durak.Game.Web.Definitions.Mapping;
using Durak.Game.Web.Endpoints.MovesEndpoints.ViewModels;

namespace Durak.Game.Web.Endpoints.MovesEndpoints
{
    public class MoveMapperConfiguration : Profile
    {
        public MoveMapperConfiguration()
        {
            CreateMap<MoveCreateViewModel, Move>()
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(x => x.Player, x => x.Ignore())
                .ForMember(x => x.Card, x => x.Ignore())
                .ForMember(x => x.Round, x => x.Ignore());
            
            CreateMap<Move, MoveViewModel>();

            CreateMap<IPagedList<Move>, IPagedList<MoveViewModel>>()
                .ConvertUsing<PagedListConverter<Move, MoveViewModel>>();
        }
    }
}
