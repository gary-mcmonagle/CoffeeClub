using CoffeeClub.Core.Api.Services;
using CoffeeClub.Domain.Dtos.Messages;
using Microsoft.AspNetCore.SignalR;

namespace CoffeeClub.Core.Api.Hubs;

public class OrderHub : Hub
{
    private readonly IHubUserConnectionProviderService<OrderHub> _hubUserConnectionProviderService;
    private readonly IOrderDispatchService _orderDispatchService;

    public OrderHub(IHubUserConnectionProviderService<OrderHub> hubUserConnectionProviderService, IOrderDispatchService orderDispatchService)
    {
        _hubUserConnectionProviderService = hubUserConnectionProviderService;
        _orderDispatchService = orderDispatchService;
    }

    public override Task OnConnectedAsync()
    {
        var user = Context.User;
        var userId = user.Claims.Where(x => x.Type == "id").FirstOrDefault()?.Value;
        _hubUserConnectionProviderService.AddConnectionForUser(Guid.Parse(userId), Context.ConnectionId);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _hubUserConnectionProviderService.RemoveConnection(Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }


    public async Task UpdateOrder(OrderUpdateDto message)
    {
        var user = Context.User;
        var userId = user.Claims.Where(x => x.Type == "id").FirstOrDefault()?.Value;
        await _orderDispatchService.UpdateOrder(message.OrderId, message.OrderStatus, Guid.Parse(userId));
    }
}
