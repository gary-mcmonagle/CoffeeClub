using CoffeeClub.Domain.Enumerations;

public record OrderUpdateMessage
{
    public OrderStatus Status { get; init; }
    public Guid OrderId { get; init; }
}