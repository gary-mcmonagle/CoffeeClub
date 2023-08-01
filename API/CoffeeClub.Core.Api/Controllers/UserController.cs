using AutoMapper;
using CoffeeClub.Domain.Dtos.Response;
using CoffeeClub.Domain.Enumerations;
using CoffeeClub.Domain.Models;
using CoffeeClub.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeClub.Core.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserRepository _userRepository;
    private IMapper _mapper;

    public UserController(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    [HttpPost(Name = "Create")]
    public async Task Post(AuthProvider authProvider)
    {
        await _userRepository.CreateAsync(_mapper.Map<User>(authProvider));
    }

    [HttpGet]
    public UserProfileDto GetUser() => new UserProfileDto { IsWorker = User.IsInRole("CoffeeClubWorker") };
}
