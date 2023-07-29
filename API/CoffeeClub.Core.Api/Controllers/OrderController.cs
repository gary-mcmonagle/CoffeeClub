using AutoMapper;
using CoffeeClub.Core.Api.Extensions;
using CoffeeClub.Domain.Dtos.Request;
using CoffeeClub.Domain.Dtos.Response;
using CoffeeClub.Domain.Enumerations;
using CoffeeClub.Domain.Models;
using CoffeeClub.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeClub.Core.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private IOrderRepository _orderRepository;
    private IUserRepository _userRepository;
    private ICoffeeBeanRepository _coffeeBeanRepository;
    private IMapper _mapper;

    public OrderController(IOrderRepository orderRepository, IMapper mapper, IUserRepository userRepository, ICoffeeBeanRepository coffeeBeanRepository)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _coffeeBeanRepository = coffeeBeanRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<OrderDto>> Get()
    {
        var userId = User.GetUserId();
        var orders = await _orderRepository.GetForUser(userId);
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateOrderDto createOrderDto)
    {
        var userId = User.GetUserId();
        var user = await _userRepository.GetAsync(userId);
        var drinks = await Task.WhenAll(createOrderDto.Drinks.Select(GetDrinkOrder));
        var order = new Order { User = user!, DrinkOrders = drinks, Status = OrderStatus.Pending };
        await _orderRepository.CreateAsync(order);
        return Ok();
    }

    private async Task<DrinkOrder> GetDrinkOrder(CreateDrinkOrderDto drinkOrder)
    {
        var bean = await _coffeeBeanRepository.GetAsync(drinkOrder.CoffeeBeanId);
        return new DrinkOrder { CoffeeBean = bean!, Drink = drinkOrder.Drink, MilkType = drinkOrder.MilkType };
    }
}
