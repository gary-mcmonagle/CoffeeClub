using System.Net.Http.Json;
using System.Text.Json.Serialization;
using CoffeeClub_Core_Functions.CustomConfiguration.Authorization;
using CoffeeClub_Core_Functions.Extensions;

namespace CoffeeClub_Core_Functions;

public class OrderHubApi
{
    private static readonly HttpClient HttpClient = new();
    private static string Etag = string.Empty;
    private static int StarCount = 0;

    private readonly IUserRepository _userRepository;

    public OrderHubApi(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [AllowAnonymous]
    [Function("index")]
    public static HttpResponseData GetHomePage([HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequestData req)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.WriteString(File.ReadAllText("content/index.html"));
        response.Headers.Add("Content-Type", "text/html");
        return response;
    }

    [Function("negotiate")]
    public async Task<HttpResponseData> Negotiate([HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequestData req,
        [SignalRConnectionInfoInput(HubName = "serverless", UserId ="{headers.userId}")] string connectionInfo)
    {
        var h = req.Headers.GetValues("userid");
        var user = await req.FunctionContext.GetUser(_userRepository);
        if(user.Id.ToString() != h.First())
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

    [Function("broadcast")]
    [AllowAnonymous]
    [SignalROutput(HubName = "serverless")]
    public static async Task<SignalRMessageAction> Broadcast([TimerTrigger("*/5 * * * * *")] TimerInfo timerInfo)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/repos/azure/azure-signalr");
        request.Headers.UserAgent.ParseAdd("Serverless");
        request.Headers.Add("If-None-Match", Etag);
        var response = await HttpClient.SendAsync(request);
        if (response.Headers.Contains("Etag"))
        {
            Etag = response.Headers.GetValues("Etag").First();
        }
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var result = await response.Content.ReadFromJsonAsync<GitResult>();
            if (result != null)
            {
                StarCount = result.StarCount;
            }
        }
        return new SignalRMessageAction("newMessage", new object[] { $"Current star count of https://github.com/Azure/azure-signalr is: {StarCount}" });
    }


    private class GitResult
    {
        [JsonPropertyName("stargazers_count")]
        public int StarCount { get; set; }
    }
}
