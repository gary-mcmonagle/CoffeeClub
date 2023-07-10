using AutoMapper;
using CoffeeClub.Domain.MappingProfiles;

namespace CoffeeClub.Core.Api.CustomConfiguration;

public static class AddAutoMapperCustomConfiguration
{
    public static void AddAutoMapper(this IServiceCollection services)
    {
        // Auto Mapper Configurations
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new CoffeeBeanProfile());
            mc.AddProfile(new UserProfile());

        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
}
