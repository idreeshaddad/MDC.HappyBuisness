using AutoMapper;
using MDC.HappyBuisness.Entities;
using MDC.HappyBuisness.Web.Models.Classifications;

namespace MDC.HappyBuisness.Web.AutoMapperProfiles
{
    public class ClassificationAutoMapperProfile : Profile
    {
        public ClassificationAutoMapperProfile()
        {
            CreateMap<Classification, ClassificationViewModel>().ReverseMap();
        }
    }
}
