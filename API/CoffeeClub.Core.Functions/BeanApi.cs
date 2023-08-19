using System.Collections.Generic;
using System.Net;
using CoffeeClub_Core_Functions.Middleware;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace CoffeeClub.Core.Functions
{
    public class BeanApi
    {
        private readonly ILogger _logger;

        public BeanApi(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<BeanApi>();
        }

        [Function("BeanApi")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")]
            HttpRequestData req)
        {
            var prinFeat = req.FunctionContext.Features.Get<JwtPrincipalFeature>();
            var claims = prinFeat?.Principal.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString($"Welcome to Azure Functions Gary!, your claims are:\n {string.Join("\n", claims)}");

            return response;
        }
    }
}
