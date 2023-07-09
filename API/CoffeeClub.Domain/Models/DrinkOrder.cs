using CoffeeBeanClub.Domain.Models;
using CoffeeClub.Domain.Enumerations;

namespace CoffeeClub.Domain.Models;

public record DrinkOrder : BaseModel
{
    public required CoffeeBean CoffeeBean { get; init; }
    public required Drink Drink { get; init; }

    public MilkType? MilkType { get; init; }
}
