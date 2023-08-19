using System.Collections.Generic;
using System.Net;
using CoffeeClub.Domain.Repositories;
using CoffeeClub_Core_Functions.Middleware;
using CoffeeClub_Core_Functions.Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

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

        [Function("BeanApi")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")]
            HttpRequestData req)
        {
            var user = req.FunctionContext.GetAuthenticatedUser();
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString($"Welcome to Azure Functions Gary!, id = {user?.Id}");

            return response;
        }
    }
}
