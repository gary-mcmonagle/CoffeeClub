using CoffeeClub.Domain.Enumerations;

namespace CoffeeClub.Domain.Models;

public record User : BaseModel
{
    public AuthProvider AuthProvider { get; init; } = AuthProvider.AzureActiveDirectory;
}