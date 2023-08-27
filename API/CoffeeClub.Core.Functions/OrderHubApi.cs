using System.Net.Http.Json;
using System.Text.Json.Serialization;
using CoffeeClub_Core_Functions.CustomConfiguration.Authorization;

namespace CoffeeClub_Core_Functions;

public class OrderHubApi
{
    private static readonly HttpClient HttpClient = new();
    private static string Etag = string.Empty;
    private static int StarCount = 0;

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
    public static HttpResponseData Negotiate([HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequestData req,
        [SignalRConnectionInfoInput(HubName = "serverless", UserId = "{headers.userId}")] string connectionInfo)
    {
        var h = req.Headers.FirstOrDefault(h => h.Key == "user-id");
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json");
        response.WriteString(connectionInfo);
        return response;
    }

    private class GitResult
    {
        [JsonPropertyName("stargazers_count")]
        public int StarCount { get; set; }
    }
}
