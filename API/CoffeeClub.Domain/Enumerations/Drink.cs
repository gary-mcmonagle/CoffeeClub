using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoffeeClub.Domain.Enumerations;

[JsonConverter(typeof(StringEnumConverter))]
public enum Drink
{
    Filter,
    Espresso,
    Cappuccino,
    Latte,
    IcedLatte,
    IcedCappuccino,
}
