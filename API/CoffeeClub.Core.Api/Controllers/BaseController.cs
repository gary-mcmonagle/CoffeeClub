using Microsoft.AspNetCore.Mvc;

namespace CoffeeClub.Core.Api.Controllers;

public class BaseController : ControllerBase
{
    protected Guid UserId =>
        User.Claims.FirstOrDefault(c => c.Type == "id")?.Value is null ?
         Guid.Empty :
         Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value);
    protected bool IsWorker => User.IsInRole("CoffeeClubWorker");
}
