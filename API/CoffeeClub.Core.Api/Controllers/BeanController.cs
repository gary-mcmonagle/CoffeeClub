using AutoMapper;
using CoffeeBeanClub.Domain.Models;
using CoffeeClub.Domain.Dtos.Request;
using CoffeeClub.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeClub.Core.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BeanController
{
    private ICoffeeBeanRepository _coffeeBeanRepository;
    private IMapper _mapper;

    public BeanController(ICoffeeBeanRepository coffeeBeanRepository, IMapper mapper)
    {
        _coffeeBeanRepository = coffeeBeanRepository;
        _mapper = mapper;
    }

    [HttpGet(Name = "Blah")]
    public async Task<IEnumerable<CoffeeBean>> Get()
    {
        return await _coffeeBeanRepository.GetAllAsync();
    }
}
