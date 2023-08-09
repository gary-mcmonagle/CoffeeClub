using Microsoft.AspNetCore.Mvc;

namespace CoffeeClub.Core.Api.Controllers;

public class DevController : ControllerBase
{
    [HttpGet]
    [Route("dev")]
    public IActionResult Dev()
    {
        return Ok(new { Hello = "World" });
    }
}