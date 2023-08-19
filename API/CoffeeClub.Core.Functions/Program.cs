using CoffeClub.Infrastructure;
using CoffeeClub.Domain.Repositories;
using CoffeeClub.Infrastructure.Repositories;
using CoffeeClub_Core_Functions.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults((context, builder) =>
    {
        builder.UseMiddleware<AuthenticationMiddleware>();
    })
    .ConfigureServices(services =>
    {
        services.AddDbContext<CoffeeClubContext>(
    options => options.UseSqlServer("Server=localhost;User Id=SA;Password=your_password1234;Database=CoffeeClub;TrustServerCertificate=true"));
        services.AddScoped<IUserRepository, UserRepository>();
    })
    .Build();

host.Run();
