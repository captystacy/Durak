using AutoMapper;
using Calabonga.UnitOfWork;
using Durak.Game.Domain;
using Durak.Game.Web.Definitions.Mapping;
using Durak.Game.Web.Endpoints.RoundsEndpoints.ViewModels;

namespace Durak.Game.Web.Endpoints.RoundsEndpoints
{
    public class RoundMapperConfiguration : Profile
    {
        public RoundMapperConfiguration()
        {
            CreateMap<Round, RoundViewModel>();

            CreateMap<IPagedList<Round>, IPagedList<RoundViewModel>>()
                .ConvertUsing<PagedListConverter<Round, RoundViewModel>>();
        }
    }
}
