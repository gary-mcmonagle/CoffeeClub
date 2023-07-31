using AutoMapper;
using CoffeeBeanClub.Domain.Models;
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
    public async Task<OrderCreatedDto> Create(CreateOrderDto createOrderDto)
    {
        var userId = User.GetUserId();
        var user = await _userRepository.GetAsync(userId);
        var allBeans = await _coffeeBeanRepository.GetAllAsync();
        var drinks = createOrderDto.Drinks.Select(d => GetDrinkOrder(d, allBeans)).ToList();
        var order = new Order { User = user!, DrinkOrders = drinks, Status = OrderStatus.Pending };
        var orderCreated = await _orderRepository.CreateAsync(order);
        return new OrderCreatedDto { Id = orderCreated.Id };
    }

    private DrinkOrder GetDrinkOrder(CreateDrinkOrderDto drinkOrder, IEnumerable<CoffeeBean> beans)
    {
        var bean = beans.FirstOrDefault(b => b.Id == drinkOrder.CoffeeBeanId);
        var drink = drinkOrder.Drink;
        if (drinkOrder.IsIced)
        {
            var gotIce = IcedCoffeeHelper.IcedCoffeeMap.TryGetValue(drinkOrder.Drink, out var icedDrink);
            if (!gotIce)
            {
                throw new Exception("Iced coffee not found");
            }
            drink = icedDrink;
        }
        return new DrinkOrder { CoffeeBean = bean!, Drink = drink, MilkType = drinkOrder.MilkType };
    }
}
