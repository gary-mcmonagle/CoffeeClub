using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoffeeClub.Domain.Enumerations;

[JsonConverter(typeof(StringEnumConverter))]

public enum Roast
{
    Light,
    Medium,
    Dark,
}
