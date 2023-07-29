using CoffeeClub.Domain.Enumerations;

namespace CoffeeClub.Domain.Dtos.Request;

public record CreateOrderDto
{
    public required IEnumerable<CreateDrinkOrderDto> Drinks { get; init; }
}

public record CreateDrinkOrderDto
{
    public Guid CoffeeBeanId { get; init; }
    public Drink Drink { get; init; }
    public MilkType? MilkType { get; init; }
}
