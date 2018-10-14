using AutoMapper;

namespace DotNetPerformance.Business.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Entities.Product, Api.Models.Product>().ReverseMap();
        }
    }
}
