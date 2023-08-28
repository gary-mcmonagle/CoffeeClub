namespace CoffeeClub_Core_Functions.OutputBindings;

public record OrderCreatedOutputBinding
{
    [QueueOutput("myQueue")]
    public OrderUpdateMessage Message { get; set; }

    public HttpResponseData HttpResponse { get; set; }
}
