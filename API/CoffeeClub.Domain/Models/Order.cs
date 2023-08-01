using CoffeeClub.Domain.Enumerations;

namespace CoffeeClub.Domain.Models;

public record Order : BaseModel
{
    public required User User { get; init; }

    public User? AssignedTo { get; set; }
    public required IEnumerable<DrinkOrder> DrinkOrders { get; init; }

    public required OrderStatus Status { get; set; } = OrderStatus.Pending;
}
