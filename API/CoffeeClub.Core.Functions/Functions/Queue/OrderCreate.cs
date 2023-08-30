namespace CoffeeClub_Core_Functions.Functions.Queue;

public class OrderCreate
{
    [SignalROutput(HubName = Constants.HubName)]
    [Function(nameof(OnOrderCreate))]
    public SignalRMessageAction OnOrderCreate(
    [QueueTrigger(Constants.OrderCreatedQueueName)] OrderDto message) => new SignalRMessageAction("OrderCreated", new object[] { message })
    {
        GroupName = "workers"
    };
}
