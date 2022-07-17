using AutoMapper;
using MDC.HappyBuisness.Entities;
using MDC.HappyBuisness.Web.Models.Pharmacists;

namespace MDC.HappyBuisness.Web.AutoMapperProfiles
{
    public class PharmacistAutoMapperProfile : Profile
    {
        public PharmacistAutoMapperProfile()
        {
            CreateMap<Pharmacist, PharmacistListViewModel>();
            CreateMap<Pharmacist, PharmacistDetailsViewModel>();
            CreateMap<Pharmacist, PharmacistViewModel>().ReverseMap();
        }
    }
}
