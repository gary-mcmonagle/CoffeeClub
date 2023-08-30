using CoffeClub.Infrastructure;
using CoffeeClub.Domain.Repositories;
using CoffeeClub.Infrastructure.Repositories;
using CoffeeClub_Core_Functions.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CoffeeClub_Core_Functions.CustomConfiguration;
using Newtonsoft.Json.Converters;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Azure.Core.Serialization;
using Newtonsoft.Json.Serialization;
using CoffeeClub.Infrastructure.Dapper;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults((context, builder) =>
    {
        var settings = NewtonsoftJsonObjectSerializer.CreateJsonSerializerSettings();
        settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        settings.Converters.Add(new StringEnumConverter());
        builder.UseNewtonsoftJson(settings);

        // builder.UseMiddleware<AuthenticationMiddleware>();
        // builder.UseMiddleware<AuthorizationMiddleware>();
        // builder.UseMiddleware<UserProviderMiddleware>();
        builder.UseWhen<AuthenticationMiddleware>((context) => context.IsHttpTrigger());
        builder.UseWhen<AuthorizationMiddleware>((context) => context.IsHttpTrigger());

    })
    .ConfigureServices(services =>
    {
        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        });
        services.AddDbContext<CoffeeClubContext>(
            options => options.UseSqlServer("Server=localhost;User Id=SA;Password=your_password1234;Database=CoffeeClub;TrustServerCertificate=true"), ServiceLifetime.Scoped);
        services.AddRepositories();
        services.AddAutoMapper();
        services.AddSingleton(new DapperContext("Server=localhost;User Id=SA;Password=your_password1234;Database=CoffeeClub;TrustServerCertificate=true"));
    })
    .Build();

host.Run();
