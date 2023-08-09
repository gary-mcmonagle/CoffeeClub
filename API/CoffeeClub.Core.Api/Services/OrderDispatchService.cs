using CoffeeClub.Core.Api.Hubs;
using CoffeeClub.Domain.Dtos.Messages;
using CoffeeClub.Domain.Dtos.Response;
using CoffeeClub.Domain.Enumerations;
using CoffeeClub.Domain.Models;
using CoffeeClub.Domain.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace CoffeeClub.Core.Api.Services;

public class OrderDispatchService : IOrderDispatchService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;
    private readonly IHubUserConnectionProviderService<OrderHub> _hubUserConnectionProviderService;
    private readonly IHubContext<OrderHub> _hubContext;

    public OrderDispatchService(IOrderRepository orderRepository, IUserRepository userRepository, IHubUserConnectionProviderService<OrderHub> hubUserConnectionProviderService, IHubContext<OrderHub> hubContext)
    {
        _orderRepository = orderRepository;
        _userRepository = userRepository;
        _hubUserConnectionProviderService = hubUserConnectionProviderService;
        _hubContext = hubContext;
    }

    public async Task OrderCreated(OrderDto order, Guid senderId)
    {
        var workerConnections = _hubUserConnectionProviderService.GetAllWorkerConnections();
        await _hubContext.Clients.Clients(workerConnections).SendAsync("OrderCreated", order);
    }

    public async Task UpdateOrder(Guid orderId, OrderStatus orderStatus, Guid senderId)
    {
        var order = await _orderRepository.GetAsync(orderId);
        var updater = await _userRepository.GetAsync(senderId);
        if (order == null)
        {
            throw new Exception($"Order with id {orderId} not found");
        }
        order!.Status = orderStatus;
        order.AssignedTo = updater;
        if (orderStatus == OrderStatus.Ready)
        {
            order.AssignedTo = null;
        }
        var userConnections = _hubUserConnectionProviderService.GetConnectionsForUserAsync(order.User.Id);
        var workerConnections = _hubUserConnectionProviderService.GetAllWorkerConnections();

        await _hubContext.Clients.Clients(userConnections.Concat(workerConnections)).SendAsync("OrderUpdated",
            new OrderUpdateDto { OrderId = orderId, OrderStatus = orderStatus });


        await _orderRepository.UpdateAsync(order);
    }
}
