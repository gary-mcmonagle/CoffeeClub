using CoffeeClub.Domain.Dtos.Messages;

namespace CoffeeClub_Core_Functions.Functions.Queue;

public class OrderUpdate
{
    private readonly IOrderRepository _orderRepository;

    public OrderUpdate(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [Function(nameof(OnOrderUpdate))]
    public async Task<OrderUpdateOutputBinding> OnOrderUpdate(
            [QueueTrigger(Constants.OrderUpdateQueueName)] OrderUpdateMessage message)
    {
        var order = await _orderRepository.GetAsync(message.OrderId);
        if (order.Status != message.Status)
        {
            order.Status = message.Status;
            await _orderRepository.UpdateAsync(order);
        }
        var dto = new OrderUpdateDto
        {
            OrderId = order.Id,
            OrderStatus = order.Status
        };
        return new OrderUpdateOutputBinding
        {
            SendToClient = new SignalRMessageAction("ClientOrderUpdate", new object[] { dto })
            {
                UserId = order.User.Id.ToString()
            },
            SendToWorkers = new SignalRMessageAction("WorkerOrderUpdate", new object[] { dto })
            {
                GroupName = "workers"
            }
        };
    }
}
