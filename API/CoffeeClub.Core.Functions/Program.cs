using CoffeClub.Infrastructure;
using CoffeeClub.Domain.Repositories;
using CoffeeClub.Infrastructure.Repositories;
using CoffeeClub_Core_Functions.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CoffeeClub_Core_Functions.CustomConfiguration;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults((context, builder) =>
    {
        builder.UseMiddleware<AuthenticationMiddleware>();
        builder.UseMiddleware<UserProviderMiddleware>();
    })
    .ConfigureServices(services =>
    {
        services.AddDbContext<CoffeeClubContext>(
            options => options.UseSqlServer("Server=localhost;User Id=SA;Password=your_password1234;Database=CoffeeClub;TrustServerCertificate=true"));
        services.AddRepositories();
    })
    .Build();

host.Run();
