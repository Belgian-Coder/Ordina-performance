using AutoMapper;

namespace DotNetPerformance.Business.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Entities.Order, Api.Models.Order>().ReverseMap();
        }
    }
}
