using System.Security.Claims;

namespace CoffeeClub.Core.Api.Extensions;

public static class ClaimsExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var userId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

        if (userId is null)
        {
            throw new Exception("User id not found in claims");
        }

        return Guid.Parse(userId);
    }
}
