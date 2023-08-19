using CoffeeClub.Domain.Repositories;
using CoffeeClub.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeClub_Core_Functions.CustomConfiguration;

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
