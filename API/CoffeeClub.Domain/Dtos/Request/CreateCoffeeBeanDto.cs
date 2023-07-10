using CoffeeClub.Domain.Enumerations;

namespace CoffeeClub.Domain.Dtos.Request;

public record CreateCoffeeBeanDto
{
    public required string Name { get; init; }
    public required Roast Roast { get; init; }
    public required string Description { get; init; } = string.Empty;

    public required bool InStock { get; init; }
}
