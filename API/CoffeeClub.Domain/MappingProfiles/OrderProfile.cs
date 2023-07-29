using AutoMapper;
using CoffeeClub.Domain.Dtos.Response;
using CoffeeClub.Domain.Models;

namespace CoffeeClub.Domain.MappingProfiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>()
        .ForMember(x => x.Drinks, x => x.MapFrom(y => y.DrinkOrders));

        CreateMap<DrinkOrderDto, DrinkOrder>();
        CreateMap<DrinkOrder, DrinkOrderDto>();

    }
}
