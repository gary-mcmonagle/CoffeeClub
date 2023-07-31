using System.Text.Json.Serialization;
using CoffeeClub.Domain.Enumerations;

namespace CoffeeClub.Domain.Dtos.Request;

public record CreateOrderDto
{
    public required IEnumerable<CreateDrinkOrderDto> Drinks { get; init; }
}

public record CreateDrinkOrderDto
{
    public Guid CoffeeBeanId { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Drink Drink { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MilkType? MilkType { get; init; }

    public required bool IsIced { get; init; }
}
