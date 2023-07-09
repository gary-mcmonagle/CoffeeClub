using CoffeeClub.Domain.Enumerations;
using CoffeeClub.Domain.Models;

namespace CoffeeBeanClub.Domain.Models;

public record CoffeeBean : BaseModel
{
    public required string Name { get; init; }
    public required Roast Roast { get; init; }
    public required string Description { get; init; } = string.Empty;

    public required bool InStock { get; init; }
}
