using System.Collections.Generic;
using System.Net;
using CoffeeClub.Domain.Repositories;
using CoffeeClub_Core_Functions.Middleware;
using CoffeeClub_Core_Functions.Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using CoffeeClub_Core_Functions.CustomConfiguration.Authorization;
using CoffeeBeanClub.Domain.Models;

namespace CoffeeClub.Core.Functions
{
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
        public async Task<HttpResponseData> GetBean(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "bean")]
            HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            var beans = await _beanRepository.GetAllAsync();
            await response.WriteAsJsonAsync(beans);
            return response;
        }
    }
}
