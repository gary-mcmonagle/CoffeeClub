using CoffeeClub.Domain.Enumerations;

namespace CoffeeClub.Domain.Dtos.Response;

public record MenuDto
{
    public IEnumerable<MenuDrinkDto> Drinks { get; init; } = new List<MenuDrinkDto>();
    public IEnumerable<CoffeeBeanMenuDto> CoffeeBeans { get; init; } = new List<CoffeeBeanMenuDto>();

    public IEnumerable<MilkType> Milks { get; init; } = new List<MilkType>();


}

public record MenuDrinkDto
{
    public required Drink Name { get; init; }
    public bool CanBeIced { get; init; }
    public bool RequiresMilk { get; init; }
}

public record CoffeeBeanMenuDto
{
    public required string Name { get; init; }
    public required Guid Id { get; init; }
}