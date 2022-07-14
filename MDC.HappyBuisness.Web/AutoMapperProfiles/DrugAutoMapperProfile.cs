using AutoMapper;
using MDC.HappyBuisness.Entities;
using MDC.HappyBuisness.Web.Models.Drugs;

namespace MDC.HappyBuisness.Web.AutoMapperProfiles
{
    public class DrugAutoMapperProfile : Profile
    {
        public DrugAutoMapperProfile()
        {
            CreateMap<Drug, DrugListViewModel>();
            CreateMap<Drug, DrugDetailsViewModel>();
            CreateMap<Drug, DrugViewModel>().ReverseMap();
        }
    }
}
