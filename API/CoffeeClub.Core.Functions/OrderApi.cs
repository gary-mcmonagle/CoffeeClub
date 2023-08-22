using AutoMapper;
using CoffeeClub.Domain.Enumerations;
using CoffeeClub_Core_Functions.Extensions;

namespace CoffeeClub_Core_Functions;

public class OrderApi
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICoffeeBeanRepository _coffeeBeanRepository;
    private readonly IMapper _mapper;

    public OrderApi(ICoffeeBeanRepository coffeeBeanRepository, IMapper mapper, IOrderRepository orderRepository, IUserRepository userRepository)
    {
        _coffeeBeanRepository = coffeeBeanRepository;
        _mapper = mapper;
        _orderRepository = orderRepository;
        _userRepository = userRepository;
    }

    [Function(nameof(GetOrder))]
    [OpenApiOperation(operationId: nameof(GetOrder))]
    [OpenApiResponseWithBody(
    statusCode: HttpStatusCode.OK,
    contentType: "application/json",
    bodyType: typeof(IEnumerable<OrderDto>))]
    public async Task<HttpResponseData> GetOrder(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "order")]
            HttpRequestData req)
    {
        var user = req.FunctionContext.GetAuthenticatedUser();
        var response = req.CreateResponse(HttpStatusCode.OK);
        var orders = await _orderRepository.GetForUser(user.Id);
        var dtos = _mapper.Map<IEnumerable<OrderDto>>(orders.Where(o => o.Status != OrderStatus.Ready));
        await response.WriteAsJsonAsync(dtos);
        return response;
    }

    private DrinkOrder GetDrinkOrder(CreateDrinkOrderDto drinkOrder, IEnumerable<CoffeeBean> beans)
    {
        var bean = beans.FirstOrDefault(b => b.Id == drinkOrder.CoffeeBeanId);
        var drink = drinkOrder.Drink;
        if (drinkOrder.IsIced)
        {
            var gotIce = IcedCoffeeHelper.IcedCoffeeMap.TryGetValue(drinkOrder.Drink, out var icedDrink);
            if (!gotIce)
            {
                throw new Exception("Iced coffee not found");
            }
            drink = icedDrink;
        }
        return new DrinkOrder { CoffeeBean = bean!, Drink = drink, MilkType = drinkOrder.MilkType };
    }
}
