using System.Security.Claims;
namespace CoffeeClub.Core.Api.CustomConfiguration;

public static class CustomClaimTypes
{
    public const string ExternalIdentityId = "sub";
    public const string UserId = "id";
    public const string AuthProvider = "authProvider";

}
