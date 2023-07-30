using CoffeeClub.Domain.Enumerations;
using Newtonsoft.Json;

namespace CoffeeClub.Domain.Dtos.Response;

public record MenuDto
{
    [JsonProperty(Required = Required.Always)]
    public required IEnumerable<MenuDrinkDto> Drinks { get; init; } = new List<MenuDrinkDto>();
    [JsonProperty(Required = Required.Always)]

    public required IEnumerable<CoffeeBeanMenuDto> CoffeeBeans { get; init; } = new List<CoffeeBeanMenuDto>();
    [JsonProperty(Required = Required.Always)]
    public required IEnumerable<MilkType> Milks { get; init; } = new List<MilkType>();
}

public record MenuDrinkDto
{
    [JsonProperty(Required = Required.Always)]
    public required Drink Name { get; init; }
    [JsonProperty(Required = Required.Always)]
    public bool CanBeIced { get; init; }

    [JsonProperty(Required = Required.Always)]
    public required bool RequiresMilk { get; init; }
}

public record CoffeeBeanMenuDto
{
    [JsonProperty(Required = Required.Always)]
    public required string Name { get; init; }

    [JsonProperty(Required = Required.Always)]
    public required Guid Id { get; init; }
}