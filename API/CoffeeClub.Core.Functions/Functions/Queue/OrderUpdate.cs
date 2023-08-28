namespace CoffeeClub_Core_Functions.Functions.Queue;

public class OrderUpdate
{
    [AllowAnonymous]
    [Function(nameof(OnOrderUpdate))]

    [SignalROutput(HubName = "serverless")]
    public async Task<SignalRMessageAction> OnOrderUpdate(
            [QueueTrigger("myQueue")] OrderUpdateMessage message)
    {
        return new SignalRMessageAction("newMessage", new object[] { message });
    }
}
