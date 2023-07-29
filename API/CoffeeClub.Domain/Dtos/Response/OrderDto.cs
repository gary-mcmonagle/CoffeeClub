using CoffeeBeanClub.Domain.Models;
using CoffeeClub.Domain.Enumerations;

namespace CoffeeClub.Domain.Dtos.Response;

public record OrderDto
{
    public required IEnumerable<DrinkOrderDto> Drinks { get; init; }
    public required Guid Id { get; init; }
    public required OrderStatus Status { get; init; }

}

public record DrinkOrderDto
{
    public required CoffeeBean CoffeeBean { get; init; }
    public required Drink Drink { get; init; }
    public MilkType? MilkType { get; init; }
}
