using System.Security.Claims;
using CoffeeClub.Domain.Enumerations;
using CoffeeClub.Domain.Models;
using CoffeeClub.Domain.Repositories;

namespace CoffeeClub.Core.Api.Extensions;

public static class UserRepositoryExtensions
{
    public static async Task<User> GetOrInsert(this IUserRepository userRepository, ClaimsPrincipal user)
    {
        var sub = user.Claims.FirstOrDefault(c => c.Type == "sub")!.Value;
        var authProviderClaim = user.Claims.FirstOrDefault(c => c.Type == "authProvider")!.Value;
        AuthProvider authProvider = (AuthProvider)Convert.ToInt32(authProviderClaim);
        var existingUser = await userRepository.GetAsync(sub, authProvider);
        if (existingUser is null)
        {
            var created = await userRepository.CreateAsync(new User { AuthId = sub, AuthProvider = authProvider });
            return created;
        }
        return existingUser;
    }
}
