using System.Reflection;
using CoffeeClub.Domain.Enumerations;
using CoffeeClub.Domain.Repositories;
using CoffeeClub.Infrastructure.Dapper;
using CoffeeClub_Core_Functions.CustomConfiguration.Authorization;
using CoffeeClub_Core_Functions.Extensions;
using Dapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace CoffeeClub_Core_Functions.Middleware;

public class UserProviderMiddleware : IFunctionsWorkerMiddleware
{
    private readonly DapperContext _dapperContext;
    public UserProviderMiddleware(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        if(HasAllowAnonymousAttribute(context.GetTargetFunctionMethod()))
        {
            await next(context);
            return;
        }
        var claims = context.Features.Get<JwtPrincipalFeature>()?.Principal.Claims;
        var subClaim = claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        var userId = GetUserId(subClaim);
        await next(context);
    }

    private string? GetUserId(string subClaim)
    {
        var authId = subClaim;
        var query = "SELECT Id FROM Users where authId = @authId";
        using (var connection = _dapperContext.CreateConnection())
        {
            var id = connection?.QueryFirstOrDefault<Guid?>(query, new { authId });
            return id?.ToString();
        }
    }

        private bool HasAllowAnonymousAttribute(MethodInfo targetMethod)
    {
        var attributes = GetCustomAttributesOnClassAndMethod<AllowAnonymousAttribute>(targetMethod);
        return attributes.Any();
    }

    private static List<T> GetCustomAttributesOnClassAndMethod<T>(MethodInfo targetMethod)
    where T : Attribute
    {
        var methodAttributes = targetMethod.GetCustomAttributes<T>();
        var classAttributes = targetMethod.DeclaringType.GetCustomAttributes<T>();
        return methodAttributes.Concat(classAttributes).ToList();
    }
}
