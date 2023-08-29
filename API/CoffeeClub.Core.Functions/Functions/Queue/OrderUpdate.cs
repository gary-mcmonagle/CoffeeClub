namespace CoffeeClub_Core_Functions.Functions.Queue;

public class OrderUpdate
{
    [Function(nameof(OnOrderUpdate))]

    [SignalROutput(HubName = Constants.HubName)]
    public async Task<SignalRMessageAction> OnOrderUpdate(
            [QueueTrigger("myQueue")] OrderUpdateMessage message)
    {
        return new SignalRMessageAction("newMessage", new object[] { message });
    }
}
