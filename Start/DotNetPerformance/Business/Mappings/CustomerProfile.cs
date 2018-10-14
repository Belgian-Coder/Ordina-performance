using AutoMapper;

namespace DotNetPerformance.Business.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Entities.Customer, Api.Models.Customer>().ReverseMap();
        }
    }
}
