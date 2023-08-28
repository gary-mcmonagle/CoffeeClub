namespace CoffeeClub_Core_Functions.Functions.Api;

public class MessageNegotiationApi
{
    private readonly IUserRepository _userRepository;

    public MessageNegotiationApi(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    [Function("negotiate")]
    public async Task<HttpResponseData> Negotiate([HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequestData req,
    [SignalRConnectionInfoInput(HubName = "serverless", UserId = "{headers.userId}")] string connectionInfo)
    {
        var h = req.Headers.GetValues("userid");
        var user = await req.FunctionContext.GetUser(_userRepository);
        if (user.Id.ToString() != h.First())
        {
            var forbiddenResponse = req.CreateResponse(HttpStatusCode.Forbidden);
            forbiddenResponse.WriteString("Forbidden");
            return forbiddenResponse;
        }

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json");
        response.WriteString(connectionInfo);
        return response;
    }
}
