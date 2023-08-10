using CoffeeClub.Core.Api.CustomConfiguration;
using CoffeeClub.Core.Api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeClub.Core.Api.Controllers;

public class BaseController : ControllerBase
{
    protected Guid UserId =>
        User.Claims.GetClaim(CustomClaimTypes.UserId) is null ?
         Guid.Empty :
         Guid.Parse(User.Claims.GetClaim(CustomClaimTypes.UserId));
    protected bool IsWorker => User.IsInRole("CoffeeClubWorker");
}
