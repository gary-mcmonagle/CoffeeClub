using CoffeeClub.Domain.Enumerations;

namespace CoffeeClub.Domain.Models;

public record Order : BaseModel
{
    public required User User { get; init; }
    public required IEnumerable<DrinkOrder> DrinkOrders { get; init; }

    public required OrderStatus Status { get; init; } = OrderStatus.Pending;
}
