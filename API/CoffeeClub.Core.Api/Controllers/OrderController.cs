using System;
using AutoMapper;
using CoffeeBeanClub.Domain.Models;
using CoffeeClub.Core.Api.Extensions;
using CoffeeClub.Core.Api.Hubs;
using CoffeeClub.Core.Api.Services;
using CoffeeClub.Domain.Dtos.Request;
using CoffeeClub.Domain.Dtos.Response;
using CoffeeClub.Domain.Enumerations;
using CoffeeClub.Domain.Models;
using CoffeeClub.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CoffeeClub.Core.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICoffeeBeanRepository _coffeeBeanRepository;
    private readonly IMapper _mapper;
    private readonly IOrderDispatchService _orderDispatchService;


    public OrderController(IOrderRepository orderRepository, IMapper mapper, IUserRepository userRepository, ICoffeeBeanRepository coffeeBeanRepository, IOrderDispatchService orderDispatchService)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _coffeeBeanRepository = coffeeBeanRepository;
        _orderDispatchService = orderDispatchService;
    }

    [HttpGet]
    public async Task<IEnumerable<OrderDto>> Get()
    {
        var userId = User.GetUserId();
        var orders = await _orderRepository.GetForUser(userId);
        return _mapper.Map<IEnumerable<OrderDto>>(orders.Where(o => o.Status != OrderStatus.Ready));
    }

    [HttpPost]
    public async Task<OrderDto> Create([FromBody] CreateOrderDto createOrderDto)
    {
        var userId = User.GetUserId();
        var user = await _userRepository.GetAsync(userId);
        var allBeans = await _coffeeBeanRepository.GetAllAsync();
        var drinks = createOrderDto.Drinks.Select(d => GetDrinkOrder(d, allBeans)).ToList();
        var order = new Order { User = user!, DrinkOrders = drinks, Status = OrderStatus.Pending };
        var orderCreated = await _orderRepository.CreateAsync(order);
        var dto = _mapper.Map<OrderDto>(orderCreated);
        await _orderDispatchService.OrderCreated(dto, userId);
        return dto;
    }

    [HttpGet]
    [Authorize(Policy = "CoffeeClubWorker")]
    [Route("assignable")]
    public async Task<IEnumerable<OrderDto>> GetAll()
    {
        var userId = User.GetUserId();
        var orders = await _orderRepository.GetAllAsync();
        Func<Order, bool> filter = o => o.Status == OrderStatus.Pending || (o.AssignedTo?.Id == userId && o.Status != OrderStatus.Ready);
        return _mapper.Map<IEnumerable<OrderDto>>(orders.Where(filter));
    }


    [HttpPost]
    [Authorize(Policy = "CoffeeClubWorker")]
    [Route("{orderId}/assign")]
    public async Task<ActionResult> Assign(Guid orderId)
    {
        var userId = User.GetUserId();
        var user = await _userRepository.GetAsync(userId);
        var order = await _orderRepository.GetAsync(orderId);
        if (order is null)
        {
            return NotFound();
        }
        order.AssignedTo = user;
        order.Status = OrderStatus.Assigned;
        await _orderRepository.UpdateAsync(order);
        return Ok();
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
