namespace CoffeeClub.Core.Api.CustomConfiguration.StartupConfig;

public static class AddSwaggerCustomConfiguration
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "CoffeeClub.Core.Api", Version = "v1" });
        });
        services.AddSwaggerGenNewtonsoftSupport();

        // Auto Mapper Configurations
        // var mapperConfig = new MapperConfiguration(mc =>
        // {
        //     mc.AddProfile(new CoffeeBeanProfile());
        //     mc.AddProfile(new UserProfile());
        //     mc.AddProfile(new OrderProfile());
        // });

        // IMapper mapper = mapperConfig.CreateMapper();
        // services.AddSingleton(mapper);
    }
}
