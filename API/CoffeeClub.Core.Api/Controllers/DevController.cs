using Microsoft.AspNetCore.Mvc;

namespace CoffeeClub.Core.Api.Controllers;

public class DevController : ControllerBase
{
    [HttpGet]
    [Route("dev")]
    public IActionResult Health()
    {
        return Ok(new { Hello = "World" });
    }
}