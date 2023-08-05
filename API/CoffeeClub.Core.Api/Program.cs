using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using CoffeClub.Infrastructure;
using CoffeeClub.Core.Api.CustomConfiguration;
using CoffeeClub.Core.Api.CustomConfiguration.AppSettingsConfig;
using CoffeeClub.Core.Api.Hubs;
using CoffeeClub.Domain.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
        .AddNewtonsoftJson(options =>
        options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter()));
builder.Logging.AddFile(o => o.RootPath = o.RootPath = builder.Environment.ContentRootPath);
builder.Services.AddDbContext<CoffeeClubContext>(
    options => options.UseSqlServer("Server=localhost;User Id=SA;Password=your_password1234;Database=CoffeeClub;TrustServerCertificate=true"));
builder.Services.AddRepositories();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper();
// Add Cors
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
}));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "CoffeeClub.Core.Api", Version = "v1" });
});

var authConfig = builder.Configuration.GetSection("Authorization").Get<AuthorizationConfig>();
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
builder.Services.AddScoped<IClaimsTransformation, ClaimsTransformer>();

builder.Services.AddCors(c =>
        {
            c.AddPolicy("AllowCCORSOrigin", options => options
                .WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                );
        });

var userRepository = builder.Services.BuildServiceProvider().GetRequiredService<IUserRepository>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    })
    .AddJwtBearer(o =>
        {
            o.SecurityTokenValidators.Clear();
            o.SecurityTokenValidators.Add(new GoogleTokenValidator(authConfig.WorkerEmails!, userRepository));
            o.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];

                    // If the request is for our hub...
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) &&
                        (path.StartsWithSegments("/hub")))
                    {
                        // Read the token out of the query string
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
        });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CoffeeClubWorker", policy => policy.RequireClaim(ClaimTypes.Role, "CoffeeClubWorker"));
});
builder.Services.AddSignalR();


builder.Services.AddSwaggerGenNewtonsoftSupport();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<SignalrHub>("/hub");


// Enable Cors
app.UseCors(builder => builder
.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader().AllowCredentials());

app.Run();

