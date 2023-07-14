using AutoMapper;
using CoffeeBeanClub.Domain.Models;
using CoffeeClub.Domain.Dtos.Response;
using CoffeeClub.Domain.Dtos.Request;
using CoffeeClub.Domain.Models;

namespace CoffeeClub.Domain.MappingProfiles;

public class CoffeeBeanProfile : Profile
{
    public CoffeeBeanProfile()
    {
        CreateMap<(CreateCoffeeBeanDto dto, User user), CoffeeBean>()
            .ForMember(x => x.Id, _ => Guid.NewGuid())
            .ForMember(x => x.CreatedBy, x => x.MapFrom(y => y.user))
            .ForMember(x => x.Name, x => x.MapFrom(y => y.dto.Name))
            .ForMember(x => x.Roast, x => x.MapFrom(y => y.dto.Roast))
            .ForMember(x => x.Description, x => x.MapFrom(y => y.dto.Description))
            .ForMember(x => x.InStock, x => x.MapFrom(y => y.dto.InStock));
    }
}
