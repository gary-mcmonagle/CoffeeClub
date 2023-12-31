namespace CoffeeClub.Domain.Dtos.Response;

public record UserProfileDto
{
    public bool IsWorker { get; init; }
    public Guid Id { get; init; }
}
