using CoffeeClub.Domain.Enumerations;

namespace CoffeeClub.Domain.Models;

public record User : BaseModel
{
    public required AuthProvider AuthProvider { get; init; } = AuthProvider.Google;

    public required string AuthId { get; init; }
}