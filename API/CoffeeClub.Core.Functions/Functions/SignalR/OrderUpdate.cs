using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoffeeClub_Core_Functions.Functions.SignalR;

public class OrderUpdate
{
    [Function(nameof(OnUpdate))]
    [QueueOutput(Constants.OrderUpdateQueueName)]
    public OrderUpdateMessage OnUpdate(
[   SignalRTrigger("serverless", "messages", "OrderUpdate", parameterNames: new[] { "dtoString" })]
        SignalRInvocationContext invocationContext, string content, FunctionContext functionContext, string dtoString)
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new JsonStringEnumConverter());
        var dto = JsonSerializer.Deserialize<OrderUpdateMessage>(dtoString, options);
        Console.WriteLine("OrderUpdate");
        return dto;
    }
}
