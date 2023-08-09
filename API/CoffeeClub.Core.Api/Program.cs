using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using CoffeClub.Infrastructure;
using CoffeeClub.Core.Api.CustomConfiguration;
using CoffeeClub.Core.Api.CustomConfiguration.AppSettingsConfig;
using CoffeeClub.Core.Api.CustomConfiguration.StartupConfig;
using CoffeeClub.Core.Api.Hubs;
using CoffeeClub.Core.Api.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var authConfig = builder.Configuration.GetSection("Authorization").Get<AuthorizationConfig>();
var coffeeClubSiteDomainConfig = builder.Configuration.GetSection("CoffeeClubSiteDomain").Get<string>();
var connectionStringConfig = builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStringConfig>();

builder.Services.AddSingleton<IHubUserConnectionProviderService<OrderHub>, HubUserConnectionProviderService<OrderHub>>();
builder.Services.AddScoped<IClaimsTransformation, ClaimsTransformer>();
builder.Services.AddScoped<IOrderDispatchService, OrderDispatchService>();
builder.Services.AddSwagger();


builder.Services.AddControllers()
        .AddNewtonsoftJson(options =>
        options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter()));
builder.Logging.AddFile(o => o.RootPath = o.RootPath = builder.Environment.ContentRootPath);
builder.Services.AddDbContext<CoffeeClubContext>(
    options => options.UseSqlServer(connectionStringConfig.DefaultConnection));
builder.Services.AddRepositories();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper();
// Add Cors
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
}));

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
// Required for SignalR
builder.Services.AddCors(c =>
        {
            c.AddPolicy("AllowCCORSOrigin", options => options
                .WithOrigins(coffeeClubSiteDomainConfig!)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .SetIsOriginAllowed((host) => true)
                );
        });

builder.Services.AddAuthConfig(authConfig?.WorkerEmails!);
builder.Services.AddSignalR().AddJsonProtocol(options =>
    {
        options.PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }); ;
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<OrderHub>("/hub");

app.UseCors(builder => builder
.WithOrigins(coffeeClubSiteDomainConfig!).AllowAnyMethod().AllowAnyHeader().AllowCredentials());

app.Run();

