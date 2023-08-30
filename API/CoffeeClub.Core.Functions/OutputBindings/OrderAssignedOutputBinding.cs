namespace CoffeeClub_Core_Functions.OutputBindings;

public record OrderAssignedOutputBinding
{
    [QueueOutput("myQueue")]
    public OrderUpdateMessage Message { get; set; }

    public HttpResponseData HttpResponse { get; set; }
}
