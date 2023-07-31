namespace CoffeeClub.Domain.Dtos.Response;

public record OrderCreatedDto
{
    public required Guid Id { get; init; }
}
