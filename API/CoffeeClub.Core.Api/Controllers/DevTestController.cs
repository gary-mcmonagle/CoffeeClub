using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeClub.Core.Api.Controllers;
[AllowAnonymous]
[ApiController]
[Route("[controller]")]
public class DevTestController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public ActionResult<DevTestDto> Get()
    {
        return Ok(new DevTestDto("Hello World!"));
    }
}

public record DevTestDto(string Name);
