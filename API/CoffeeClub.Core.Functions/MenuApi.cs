using System.Net;
using CoffeeClub.Domain.Dtos.Response;
using CoffeeClub.Domain.Enumerations;
using CoffeeClub.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace CoffeeClub.Core.Functions;
public class MenuApi
{
    private ICoffeeBeanRepository _coffeeBeanRepository;

    public MenuApi(ICoffeeBeanRepository coffeeBeanRepository)
    {
        _coffeeBeanRepository = coffeeBeanRepository;
    }

    [Function(nameof(GetMenu))]
    [OpenApiOperation(operationId: "GetMenu")]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(MenuDto))]
    public async Task<HttpResponseData> GetMenu(
        [Microsoft.Azure.Functions.Worker.HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "menu")]
            HttpRequestData req)
    {

        var drinks = Enum.GetValues<Drink>().Select(GetMenuItemForDrink).Where(x => x != null).ToList();
        var beans = await _coffeeBeanRepository.GetAllAsync();
        var milks = Enum.GetValues<MilkType>().ToList();

        var menu = new MenuDto
        {
            Drinks = drinks,
            CoffeeBeans = beans
                .Where(x => x.InStock)
                .Select(x => new CoffeeBeanMenuDto { Id = x.Id, Name = x.Name }),
            Milks = milks
        };
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(menu);
        return response;
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