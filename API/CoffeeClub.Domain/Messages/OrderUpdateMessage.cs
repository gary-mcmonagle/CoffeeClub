using System.Text.Json.Serialization;
using CoffeeClub.Domain.Enumerations;

public record OrderUpdateMessage
{
    [JsonPropertyName("orderStatus")]
    public OrderStatus Status { get; init; }
    [JsonPropertyName("orderId")]
    public Guid OrderId { get; init; }
}