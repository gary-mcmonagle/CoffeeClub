using AutoMapper;
using CoffeeClub.Domain.MappingProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeClub_Core_Functions.CustomConfiguration;

public static class AddAutoMapperCustomConfiguration
{
    public static void AddAutoMapper(this IServiceCollection services)
    {
        // Auto Mapper Configurations
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new CoffeeBeanProfile());
            mc.AddProfile(new UserProfile());
            mc.AddProfile(new OrderProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
}
