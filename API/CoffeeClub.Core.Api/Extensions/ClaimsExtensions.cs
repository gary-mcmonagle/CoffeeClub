using System.Security.Claims;

namespace CoffeeClub.Core.Api.Extensions;

public static class ClaimsExtensions
{

    public static string? GetClaim(this IEnumerable<Claim> claims, string type) =>
     claims.FirstOrDefault(c => c.Type == type)?.Value;
}
