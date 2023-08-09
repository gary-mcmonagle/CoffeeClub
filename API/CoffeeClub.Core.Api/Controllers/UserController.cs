using AutoMapper;
using CoffeeClub.Domain.Dtos.Response;
using CoffeeClub.Domain.Enumerations;
using CoffeeClub.Domain.Models;
using CoffeeClub.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeClub.Core.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : BaseController
{
    [HttpGet]
    public ActionResult<UserProfileDto> GetUser() =>
        Ok(new UserProfileDto() { IsWorker = IsWorker });
}
