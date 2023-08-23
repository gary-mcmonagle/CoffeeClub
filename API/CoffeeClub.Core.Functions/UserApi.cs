using System.Security.Claims;
using CoffeeClub_Core_Functions.Extensions;
using CoffeeClub_Core_Functions.Middleware;

namespace CoffeeClub_Core_Functions;

public class UserApi
{
    [Function(nameof(GetUser))]
    [OpenApiOperation(operationId: nameof(GetUser), tags: new[] { "user" })]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK,
        contentType: "application/json",
        bodyType: typeof(UserProfileDto))]
    public async Task<HttpResponseData> GetUser(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "order")]
            HttpRequestData req)
    {
        var claims = req.FunctionContext.Features.Get<JwtPrincipalFeature>()?.Principal.Claims;
        var roleClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        var isWorker = roleClaim == "CoffeeClubWorker";
        var dto = new UserProfileDto { IsWorker = isWorker };
        var response = req.CreateResponse(HttpStatusCode.OK);
        return response;
    }
}
