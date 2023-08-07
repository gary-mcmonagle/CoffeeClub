using CoffeeClub.Domain.Enumerations;

namespace CoffeeClub.Domain.Dtos.Messages;

public record OrderUpdateDto
{
    public Guid OrderId { get; set; }
    public OrderStatus OrderStatus { get; set; }
}
