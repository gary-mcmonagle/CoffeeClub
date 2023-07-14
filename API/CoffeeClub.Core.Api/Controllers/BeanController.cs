using AutoMapper;
using CoffeeBeanClub.Domain.Models;
using CoffeeClub.Core.Api.CustomConfiguration;
using CoffeeClub.Core.Api.Extensions;
using CoffeeClub.Domain.Dtos.Request;
using CoffeeClub.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeClub.Core.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class BeanController : ControllerBase
{
    private ICoffeeBeanRepository _coffeeBeanRepository;
    private IUserRepository _userRepository;
    private IMapper _mapper;

    public BeanController(ICoffeeBeanRepository coffeeBeanRepository, IMapper mapper, IUserRepository userRepository)
    {
        _coffeeBeanRepository = coffeeBeanRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    [HttpGet]
    [Authorize(Policy = "CoffeeClubWorker")]
    public async Task<IEnumerable<CoffeeBean>> Get()
    {
        return await _coffeeBeanRepository.GetAllAsync();
    }

    [HttpPost]
    [Authorize(Policy = "CoffeeClubWorker")]
    public async Task<ActionResult> Create(CreateCoffeeBeanDto createCoffeeBeanDto)
    {
        var user = await _userRepository.GetOrInsert(User);
        var newBean = await _coffeeBeanRepository.CreateAsync(_mapper.Map<CoffeeBean>((createCoffeeBeanDto, user)));
        return Ok();
    }
}
