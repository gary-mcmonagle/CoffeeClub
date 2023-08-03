using CoffeeClub.Domain.Enumerations;

namespace CoffeeClub.Domain.Dtos.Response;

public record OrderCreatedDto
{
    public required Guid Id { get; init; }
    public required OrderStatus Status { get; init; }
}
