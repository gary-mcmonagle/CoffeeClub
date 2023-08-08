namespace CoffeeClub.Core.Api.CustomConfiguration.AppSettingsConfig;

public record ConnectionStringConfig
{
    public string DefaultConnection { get; init; } = string.Empty;
}