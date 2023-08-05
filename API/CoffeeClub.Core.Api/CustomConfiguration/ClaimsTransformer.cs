using System.Security.Claims;
using CoffeeClub.Domain.Enumerations;
using CoffeeClub.Domain.Repositories;
using Microsoft.AspNetCore.Authentication;

namespace CoffeeClub.Core.Api.CustomConfiguration;

public class ClaimsTransformer : IClaimsTransformation
{

    private readonly IUserRepository _userRepository;

    public ClaimsTransformer(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var identity = (ClaimsIdentity)principal.Identity;
        var sub = identity?.Claims.First(x => x.Type == "sub").Value;
        var authProviderString = identity?.Claims.First(x => x.Type == "authProvider").Value;
        AuthProvider authProvider = (AuthProvider)Enum.Parse(typeof(AuthProvider), authProviderString);
        var user = await _userRepository.GetOrCreateAsync(sub, authProvider);
        ClaimsIdentity claimsIdentity = new ClaimsIdentity();

        claimsIdentity.AddClaim(new Claim("id", user?.Id.ToString()));
        principal.AddIdentity(claimsIdentity);
        return principal;
    }

}
