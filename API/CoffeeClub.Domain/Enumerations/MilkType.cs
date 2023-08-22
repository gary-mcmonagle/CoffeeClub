using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoffeeClub.Domain.Enumerations;

[JsonConverter(typeof(StringEnumConverter))]
public enum MilkType
{
    Dairy,
    Oat
}
