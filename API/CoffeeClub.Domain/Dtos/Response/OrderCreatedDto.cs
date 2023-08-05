using CoffeeClub.Domain.Enumerations;
using Newtonsoft.Json;

namespace CoffeeClub.Domain.Dtos.Response;

public record OrderCreatedDto
{
    [JsonProperty(Required = Required.Always)]

    public required Guid Id { get; init; }
    [JsonProperty(Required = Required.Always)]

    public required OrderStatus Status { get; init; }
}
