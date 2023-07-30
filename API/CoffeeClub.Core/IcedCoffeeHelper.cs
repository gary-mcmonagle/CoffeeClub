using CoffeeClub.Domain.Enumerations;

namespace CoffeeClub.Core;

public static class IcedCoffeeHelper
{
    public static Dictionary<Drink, Drink> IcedCoffeeMap = new()
    {
        { Drink.Latte, Drink.IcedLatte },
        { Drink.Cappuccino, Drink.IcedCappuccino }
    };
}
