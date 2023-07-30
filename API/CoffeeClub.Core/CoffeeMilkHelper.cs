using CoffeeClub.Domain.Enumerations;

namespace CoffeeClub.Core;

public static class CoffeeMilkHelper
{
    public static Dictionary<Drink, bool> RequiresCoffeeMilkMap = new()
    {
        { Drink.Filter, false },
        { Drink.Espresso, false },
        { Drink.Cappuccino, true },
        { Drink.Latte, true },
        { Drink.IcedLatte, true },
        { Drink.IcedCappuccino, true }
    };
}
