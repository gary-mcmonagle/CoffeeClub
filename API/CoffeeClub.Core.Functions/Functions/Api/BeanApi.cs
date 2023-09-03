namespace CoffeeClub_Core_Functions.Functions.Api;

public class BeanApi
{
    private readonly ILogger _logger;
    private readonly IUserRepository _userRepository;
    private readonly ICoffeeBeanRepository _beanRepository;




    public BeanApi(ILoggerFactory loggerFactory, IUserRepository userRepository, ICoffeeBeanRepository beanRepository)
    {
        _logger = loggerFactory.CreateLogger<BeanApi>();
        _userRepository = userRepository;
        _beanRepository = beanRepository;
    }

    [Function(nameof(GetBean))]
    [WorkerAuthorize]
    [OpenApiOperation(operationId: "GetBean", tags: new[] { "bean" })]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(IEnumerable<CoffeeBean>))]
    public async Task<HttpResponseData> GetBean(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "bean")]
            HttpRequestData req)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        var beans = await _beanRepository.GetAllAsync();
        await response.WriteAsJsonAsync(beans);
        return response;
    }

    [Function(nameof(CreateBean))]
    [WorkerAuthorize]
    [OpenApiRequestBody("application/json", typeof(CreateCoffeeBeanDto))]
    [OpenApiOperation(operationId: "CreateBean", tags: new[] { "bean" })]
    [OpenApiResponseWithoutBody(
        statusCode: HttpStatusCode.OK)]
    public async Task<HttpResponseData> CreateBean(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "bean")]
            HttpRequestData req, [FromBody] CreateCoffeeBeanDto createCoffeeBeanDto)
    {
        var user = await req.FunctionContext.GetUser(_userRepository);
        var coffeeBean = new CoffeeBean
        {
            Name = createCoffeeBeanDto.Name,
            CreatedBy = user!,
            Roast = createCoffeeBeanDto.Roast,
            Description = createCoffeeBeanDto.Description,
            InStock = createCoffeeBeanDto.InStock
        };
        await _beanRepository.CreateAsync(coffeeBean);
        var response = req.CreateResponse(HttpStatusCode.OK);
        return response;
    }
}
