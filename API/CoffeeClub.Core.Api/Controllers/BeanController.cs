using AutoMapper;
using CoffeeBeanClub.Domain.Models;
using CoffeeClub.Core.Api.CustomConfiguration;
using CoffeeClub.Core.Api.Extensions;
using CoffeeClub.Domain.Dtos.Request;
using CoffeeClub.Domain.Repositories;
using JsonPatch.Restrict;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeClub.Core.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class BeanController : BaseController
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

    [HttpPatch]
    [Authorize(Policy = "CoffeeClubWorker")]
    [Route("{coffeeBeanId:guid}")]
    public async Task<ActionResult> PatchBean([FromBody] JsonPatchDocument<CoffeeBean> patch, Guid coffeeBeanId)
    {
        var bean = await _coffeeBeanRepository.GetAsync(coffeeBeanId);
        if (bean is null)
        {
            return NotFound();
        }
        patch.ApplyToWithRestrictions(bean, "Description", "Roast", "InStock", "Name");
        await _coffeeBeanRepository.UpdateAsync(bean);
        return Ok();
    }


    [HttpPut]
    [Route("out-of-stock/{coffeeBeanId:guid}")]
    [Authorize(Policy = "CoffeeClubWorker")]
    public async Task<ActionResult> OutOfStock(Guid coffeeBeanId)
    {
        var coffeeBean = await _coffeeBeanRepository.GetAsync(coffeeBeanId);
        if (coffeeBean is null)
        {
            return NotFound();
        }
        coffeeBean.InStock = false;
        await _coffeeBeanRepository.UpdateAsync(coffeeBean);
        return Ok();
    }

    [HttpPost]
    [Authorize(Policy = "CoffeeClubWorker")]
    public async Task<ActionResult> Create(CreateCoffeeBeanDto createCoffeeBeanDto)
    {
        var user = await _userRepository.GetAsync(UserId);
        var coffeeBean = new CoffeeBean
        {
            Name = createCoffeeBeanDto.Name,
            CreatedBy = user!,
            Roast = createCoffeeBeanDto.Roast,
            Description = createCoffeeBeanDto.Description,
            InStock = createCoffeeBeanDto.InStock
        };
        await _coffeeBeanRepository.CreateAsync(coffeeBean);
        return Ok();
    }
}
