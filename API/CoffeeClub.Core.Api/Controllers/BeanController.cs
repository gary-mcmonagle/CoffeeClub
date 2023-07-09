using CoffeeBeanClub.Domain.Models;
using CoffeeClub.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeClub.Core.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BeanController
{
    private ICoffeeBeanRepository _coffeeBeanRepository;

    public BeanController(ICoffeeBeanRepository coffeeBeanRepository)
    {
        _coffeeBeanRepository = coffeeBeanRepository;
    }

    [HttpGet(Name = "Blah")]
    public async Task<IEnumerable<CoffeeBean>> Get()
    {
        return await _coffeeBeanRepository.GetAllAsync();
    }

    [HttpPost(Name = "Create")]
    public async Task<CoffeeBean> Post(CoffeeBean coffeeBean)
    {
        return await _coffeeBeanRepository.CreateAsync(coffeeBean);
    }

}
