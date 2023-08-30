namespace CoffeeClub_Core_Functions.OutputBindings;

public record OrderUpdateOutputBinding
{

    [SignalROutput(HubName = Constants.HubName)]
    public required SignalRMessageAction SendToClient { get; set; }

    [SignalROutput(HubName = Constants.HubName)]
    public required SignalRMessageAction SendToWorkers { get; init; }
}
