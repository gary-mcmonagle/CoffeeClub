using System.Collections.Generic;
using System.Net;
using CoffeeClub.Domain.Repositories;
using CoffeeClub_Core_Functions.Middleware;
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
            var prinFeat = req.FunctionContext.Features.Get<JwtPrincipalFeature>();
            var claims = prinFeat?.Principal.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();
            var subClaim = prinFeat?.Principal.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            var user = await _userRepository.GetAsync(subClaim!, Domain.Enumerations.AuthProvider.Google);
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString($"Welcome to Azure Functions Gary!, id = {user?.Id}");

            return response;
        }
    }
}
