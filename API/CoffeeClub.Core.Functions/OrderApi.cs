using AutoMapper;
using CoffeeClub.Domain.Enumerations;
using CoffeeClub_Core_Functions.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;


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
    [OpenApiOperation(operationId: nameof(GetOrder), tags: new[] { "order" })]
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

    [Function(nameof(GetAssignable))]
    [OpenApiOperation(operationId: nameof(GetAssignable), tags: new[] { "order" })]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(IEnumerable<OrderDto>))]
    public async Task<HttpResponseData> GetAssignable(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "order/assignable")]
            HttpRequestData req)
    {
        var userId = req.FunctionContext.GetAuthenticatedUser().Id;
        var orders = await _orderRepository.GetAllAsync();
        Func<Order, bool> filter = o => o.Status == OrderStatus.Pending || (o.AssignedTo?.Id == userId && o.Status != OrderStatus.Ready);
        var dtos = _mapper.Map<IEnumerable<OrderDto>>(orders.Where(filter));
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(dtos);
        return response;
    }

    [Function(nameof(CreateOrder))]
    [OpenApiOperation(operationId: nameof(CreateOrder), tags: new[] { "order" })]
    [OpenApiRequestBody("application/json", typeof(CreateOrderDto))]
    [OpenApiResponseWithBody(

    statusCode: HttpStatusCode.OK,
    contentType: "application/json",
    bodyType: typeof(IEnumerable<OrderDto>))]
    public async Task<HttpResponseData> CreateOrder(
    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "order")]
            HttpRequestData req, [FromBody] CreateOrderDto createOrderDto)
    {
        var userId = req.FunctionContext.GetAuthenticatedUser().Id;
        var user = await _userRepository.GetAsync(userId);
        var allBeans = await _coffeeBeanRepository.GetAllAsync();
        var drinks = createOrderDto.Drinks.Select(d => GetDrinkOrder(d, allBeans)).ToList();
        var order = new Order { User = user!, DrinkOrders = drinks, Status = OrderStatus.Pending };
        var orderCreated = await _orderRepository.CreateAsync(order);
        var dto = _mapper.Map<OrderDto>(orderCreated);
        // await _orderDispatchService.OrderCreated(dto, UserId);
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(dto);
        return response;
    }

    [Function(nameof(AssignOrder))]
    [OpenApiOperation(operationId: nameof(AssignOrder), tags: new[] { "order" })]
    [OpenApiParameter(
        name: "orderId",
        In = ParameterLocation.Path,
        Required = true,
        Type = typeof(Guid),
        Description = "The order id")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK)]
    public async Task<HttpResponseData> AssignOrder(
    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "order/assign/{orderId:guid}")]
            HttpRequestData req, Guid orderId)
    {
        var userId = req.FunctionContext.GetAuthenticatedUser().Id;
        var user = await _userRepository.GetAsync(userId);
        var order = await _orderRepository.GetAsync(orderId);
        if (order is null)
        {
            var notFoundResponse = req.CreateResponse(HttpStatusCode.NotFound);
            return notFoundResponse;
        }
        order.AssignedTo = user;
        order.Status = OrderStatus.Assigned;
        await _orderRepository.UpdateAsync(order);
        var response = req.CreateResponse(HttpStatusCode.OK);
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
