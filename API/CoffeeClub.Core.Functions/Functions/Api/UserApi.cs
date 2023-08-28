using System.Security.Claims;
using CoffeeClub_Core_Functions.Middleware;

namespace CoffeeClub_Core_Functions.Functions.Api;

public class UserApi
{
    private IUserRepository _userRepository;

    public UserApi(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [Function(nameof(GetUser))]
    [OpenApiOperation(operationId: nameof(GetUser), tags: new[] { "user" })]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(UserProfileDto))]
    public async Task<HttpResponseData> GetUser(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user")]
            HttpRequestData req)
    {
        var user = await req.FunctionContext.GetUser(_userRepository);
        var claims = req.FunctionContext.Features.Get<JwtPrincipalFeature>()?.Principal.Claims;
        var roleClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        var isWorker = roleClaim == "CoffeeClubWorker";
        var dto = new UserProfileDto { IsWorker = isWorker, Id = user.Id };
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(dto);
        return response;
    }
}
