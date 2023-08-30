namespace CoffeeClub_Core_Functions.OutputBindings;

public record OrderCreatedOutputBinding
{
    [QueueOutput(Constants.OrderCreatedQueueName)]
    public OrderDto Message { get; set; }

    public HttpResponseData HttpResponse { get; set; }
}
