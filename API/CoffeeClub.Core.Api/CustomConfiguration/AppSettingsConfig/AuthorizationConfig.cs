namespace CoffeeClub.Core.Api.CustomConfiguration.AppSettingsConfig;

public record AuthorizationConfig
{
    public required string[] WorkerEmails { get; init; }
}
