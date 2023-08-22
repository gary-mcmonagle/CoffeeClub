using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoffeeClub.Domain.Enumerations;

[JsonConverter(typeof(StringEnumConverter))]
public enum OrderStatus
{
    Pending,
    Received,
    Assigned,
    InProgress,
    Ready
}
