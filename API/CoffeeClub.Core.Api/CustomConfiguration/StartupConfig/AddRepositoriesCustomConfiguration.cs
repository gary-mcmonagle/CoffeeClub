using CoffeeClub.Domain.Repositories;
using CoffeeClub.Infrastructure.Repositories;

namespace CoffeeClub.Core.Api.CustomConfiguration.StartupConfig;

public static class AddRepositoriesCustomConfiguration
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICoffeeBeanRepository, CoffeeBeanRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
    }
}
