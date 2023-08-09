using CoffeeClub.Domain.Dtos.Response;
using CoffeeClub.Domain.Enumerations;
using CoffeeClub.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeClub.Core.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class MenuController : ControllerBase
{
    private ICoffeeBeanRepository _coffeeBeanRepository;

    public MenuController(ICoffeeBeanRepository coffeeBeanRepository)
    {
        _coffeeBeanRepository = coffeeBeanRepository;
    }

    [HttpGet]
    public async Task<MenuDto> Get()
    {
        var drinks = Enum.GetValues<Drink>().Select(GetMenuItemForDrink).Where(x => x != null).ToList();
        var beans = await _coffeeBeanRepository.GetAllAsync();
        var milks = Enum.GetValues<MilkType>().ToList();

        return new MenuDto
        {
            Drinks = drinks,
            CoffeeBeans = beans
                .Where(x => x.InStock)
                .Select(x => new CoffeeBeanMenuDto { Id = x.Id, Name = x.Name }),
            Milks = milks
        };
    }
    private MenuDrinkDto? GetMenuItemForDrink(Drink drink)
    {
        if (IcedCoffeeHelper.IcedCoffeeMap.Values.Contains(drink))
        {
            return null;
        }
        return new MenuDrinkDto
        {
            Name = drink,
            CanBeIced = IcedCoffeeHelper.IcedCoffeeMap.ContainsKey(drink),
            RequiresMilk = CoffeeMilkHelper.RequiresCoffeeMilkMap[drink]
        };
    }
}