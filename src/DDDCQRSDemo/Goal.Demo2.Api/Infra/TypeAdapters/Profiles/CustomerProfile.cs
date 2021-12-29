using AutoMapper;
using Goal.Demo2.Api.Application.Dtos.Customers;
using Goal.Demo2.Domain.Aggregates.Customers;

namespace Goal.Demo2.Api.Infra.TypeAdapters.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
