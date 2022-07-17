using AutoMapper;
using MDC.HappyBuisness.Entities;
using MDC.HappyBuisness.Web.Models.Buyers;

namespace MDC.HappyBuisness.Web.AutoMapperProfiles
{
    public class BuyerAutoMapperProfile : Profile
    {
        public BuyerAutoMapperProfile()
        {
            CreateMap<Buyer, BuyerListViewModel>();
            CreateMap<Buyer, BuyerDetailsViewModel>();
            CreateMap<Buyer, BuyerViewModel>().ReverseMap();
        }
    }
}
