using AutoMapper;
using MDC.HappyBuisness.Entities;
using MDC.HappyBuisness.Web.Models.Deals;

namespace MDC.HappyBuisness.Web.AutoMapperProfiles
{
    public class DealAutoMapperProfile : Profile
    {
        public DealAutoMapperProfile()
        {
            CreateMap<Deal, DealListViewModel>();
            CreateMap<Deal, DealDetailsViewModel>();
            CreateMap<Deal, DealViewModel>().ReverseMap();
        }
    }
}
