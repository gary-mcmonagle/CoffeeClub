using System.Security.Claims;
using CoffeeClub.Core.Api.Extensions;
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
        var sub = identity?.Claims.GetClaim(CustomClaimTypes.ExternalIdentityId);
        var authProviderString = identity?.Claims.GetClaim(CustomClaimTypes.AuthProvider);
        AuthProvider authProvider = (AuthProvider)Enum.Parse(typeof(AuthProvider), authProviderString);
        var user = await _userRepository.GetOrCreateAsync(sub, authProvider);
        ClaimsIdentity claimsIdentity = new ClaimsIdentity();

        claimsIdentity.AddClaim(new Claim(CustomClaimTypes.UserId, user?.Id.ToString()));
        principal.AddIdentity(claimsIdentity);
        return principal;
    }

}
