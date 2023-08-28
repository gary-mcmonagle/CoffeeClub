global using System.Net;
global using CoffeeClub.Domain.Dtos.Response;
global using Microsoft.Azure.Functions.Worker;
global using Microsoft.Azure.Functions.Worker.Http;
global using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
global using CoffeeClub.Domain.Repositories;
global using CoffeeClub.Domain.Models;
global using CoffeeClub.Core;
global using CoffeeClub.Domain.Dtos.Request;
global using CoffeeBeanClub.Domain.Models;
global using CoffeeClub.Domain.Enumerations;
global using CoffeeClub_Core_Functions.CustomConfiguration.Authorization;
global using CoffeeClub_Core_Functions.Extensions;
global using Microsoft.Extensions.Logging;
global using System;
global using AutoMapper;
global using Microsoft.OpenApi.Models;
global using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;
global using System.Security.Claims;
global using CoffeeClub_Core_Functions.Middleware;
global using CoffeeClub_Core_Functions.OutputBindings;




