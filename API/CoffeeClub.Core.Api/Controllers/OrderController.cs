using AutoMapper;
using CoffeeClub.Core.Api.Extensions;
using CoffeeClub.Domain.Dtos.Response;
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
    private IMapper _mapper;

    public OrderController(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<OrderDto>> Get()
    {
        var userId = User.GetUserId();
        var orders = await _orderRepository.GetForUser(userId);
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }
}
