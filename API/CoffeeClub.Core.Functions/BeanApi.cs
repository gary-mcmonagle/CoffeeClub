using System.Collections.Generic;
using System.Net;
using CoffeeClub.Domain.Repositories;
using CoffeeClub_Core_Functions.Middleware;
using CoffeeClub_Core_Functions.Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using CoffeeClub_Core_Functions.CustomConfiguration.Authorization;

namespace CoffeeClub.Core.Functions
{
    public class BeanApi
    {
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;

        public BeanApi(ILoggerFactory loggerFactory, IUserRepository userRepository)
        {
            _logger = loggerFactory.CreateLogger<BeanApi>();
            _userRepository = userRepository;
        }

        [Function(nameof(GetBean))]
        [WorkerAuthorize]
        public async Task<HttpResponseData> GetBean(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "bean")]
            HttpRequestData req)
        {
            var user = req.FunctionContext.GetAuthenticatedUser();
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString($"Welcome to Azure Functions Gary!, id = {user?.Id}");

            return response;
        }

        [Function(nameof(CreateBean))]
        [WorkerAuthorize]
        public async Task<HttpResponseData> CreateBean(
    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "bean")]
            HttpRequestData req)
        {
            var user = req.FunctionContext.GetAuthenticatedUser();
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString($"Welcome to Azure Functions Gary Create!, id = {user?.Id}");

            return response;
        }
    }
}
