using System;
using AutoMapper;
using CoffeeBeanClub.Domain.Models;
using CoffeeClub.Core.Api.Extensions;
using CoffeeClub.Core.Api.Hubs;
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
    private IOrderRepository _orderRepository;
    private IUserRepository _userRepository;
    private ICoffeeBeanRepository _coffeeBeanRepository;
    private IMapper _mapper;
    private IHubContext<OrderHub> _hubContext;

    public OrderController(IOrderRepository orderRepository, IMapper mapper, IUserRepository userRepository, ICoffeeBeanRepository coffeeBeanRepository, IHubContext<OrderHub> hubContext)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _coffeeBeanRepository = coffeeBeanRepository;
        _hubContext = hubContext;
    }

    [HttpGet]
    public async Task<IEnumerable<OrderDto>> Get()
    {
        var userId = User.GetUserId();
        var orders = await _orderRepository.GetForUser(userId);
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    [HttpPost]
    public async Task<OrderCreatedDto> Create([FromBody] CreateOrderDto createOrderDto)
    {
        var userId = User.GetUserId();
        var user = await _userRepository.GetAsync(userId);
        var allBeans = await _coffeeBeanRepository.GetAllAsync();
        var drinks = createOrderDto.Drinks.Select(d => GetDrinkOrder(d, allBeans)).ToList();
        var order = new Order { User = user!, DrinkOrders = drinks, Status = OrderStatus.Pending };
        var orderCreated = await _orderRepository.CreateAsync(order);
        return new OrderCreatedDto { Id = orderCreated.Id, Status = orderCreated.Status };
    }

    [HttpGet]
    [Authorize(Policy = "CoffeeClubWorker")]
    [Route("assignable")]

    public async Task<IEnumerable<OrderDto>> GetAll()
    {
        var orders = await _orderRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<OrderDto>>(orders.Where(o => o.Status == OrderStatus.Pending));
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
        _hubContext.Clients.All.SendAsync("ReceiveMessage", order.Id);

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
