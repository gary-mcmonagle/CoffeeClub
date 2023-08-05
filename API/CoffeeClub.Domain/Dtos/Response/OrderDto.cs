using CoffeeBeanClub.Domain.Models;
using CoffeeClub.Domain.Enumerations;
using Newtonsoft.Json;

namespace CoffeeClub.Domain.Dtos.Response;

public record OrderDto
{
    [JsonProperty(Required = Required.Always)]

    public required IEnumerable<DrinkOrderDto> Drinks { get; init; }
    [JsonProperty(Required = Required.Always)]

    public required Guid Id { get; init; }
    [JsonProperty(Required = Required.Always)]

    public required OrderStatus Status { get; init; }

}

public record DrinkOrderDto
{
    [JsonProperty(Required = Required.Always)]

    public required CoffeeBean CoffeeBean { get; init; }

    [JsonProperty(Required = Required.Always)]
    public required Drink Drink { get; init; }
    public MilkType? MilkType { get; init; }
}
