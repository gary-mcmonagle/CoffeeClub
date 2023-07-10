using AutoMapper;
using CoffeeBeanClub.Domain.Models;
using CoffeeClub.Domain.Dtos.Reponse;
using CoffeeClub.Domain.Dtos.Request;

namespace CoffeeClub.Domain.MappingProfiles;

public class CoffeeBeanProfile : Profile
{
    public CoffeeBeanProfile()
    {
        CreateMap<CreateCoffeeBeanDto, CoffeeBean>()
            .ForMember(x => x.Id, _ => Guid.NewGuid());
    }
}
