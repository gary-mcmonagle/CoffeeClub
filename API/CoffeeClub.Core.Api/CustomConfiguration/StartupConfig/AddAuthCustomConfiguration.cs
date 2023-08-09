using System.Security.Claims;
using AutoMapper;
using CoffeeClub.Domain.MappingProfiles;
using CoffeeClub.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CoffeeClub.Core.Api.CustomConfiguration.StartupConfig;

public static class AddAuthCustomConfiguration
{
    public static void AddAuthConfig(this IServiceCollection services, IEnumerable<string> workerEmails)
    {
        var userRepository = services.BuildServiceProvider().GetRequiredService<IUserRepository>();
        services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    })
    .AddJwtBearer(o =>
        {
            o.SecurityTokenValidators.Clear();
            o.SecurityTokenValidators.Add(new GoogleTokenValidator(workerEmails!, userRepository));
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
        services.AddAuthorization(options =>
        {
            options.AddPolicy("CoffeeClubWorker", policy => policy.RequireClaim(ClaimTypes.Role, "CoffeeClubWorker"));
        });
    }
}