using CoffeeClub_Core_Functions.Middleware;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults((context, builder) =>
    {
        builder.UseMiddleware<AuthenticationMiddleware>();
    })
    .Build();

host.Run();
