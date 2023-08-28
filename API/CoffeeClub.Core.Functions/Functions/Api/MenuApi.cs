namespace CoffeeClub_Core_Functions.Functions.Api;

public class MenuApi
{
    private ICoffeeBeanRepository _coffeeBeanRepository;

    public MenuApi(ICoffeeBeanRepository coffeeBeanRepository)
    {
        _coffeeBeanRepository = coffeeBeanRepository;
    }

    [Function(nameof(GetMenu))]
    [OpenApiOperation(operationId: "GetMenu", tags: new[] { "menu" })]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(MenuDto))]
    public async Task<HttpResponseData> GetMenu(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "menu")]
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
