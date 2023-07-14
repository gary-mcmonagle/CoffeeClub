using AutoMapper;
using CoffeeBeanClub.Domain.Models;
using CoffeeClub.Domain.Dtos.Response;
using CoffeeClub.Domain.Dtos.Request;
using CoffeeClub.Domain.Enumerations;
using CoffeeClub.Domain.Models;

namespace CoffeeClub.Domain.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<AuthProvider, User>()
            .ForMember(x => x.Id, _ => Guid.NewGuid())
            .ForMember(x => x.AuthProvider, s => s.MapFrom(x => x));
    }
}
