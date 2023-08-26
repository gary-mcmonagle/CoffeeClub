using System.Security.Claims;
using System.Reflection;
using CoffeeClub_Core_Functions.CustomConfiguration.Authorization;
using CoffeeClub_Core_Functions.Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using System.Net;

namespace CoffeeClub_Core_Functions.Middleware;

public class AuthorizationMiddleware : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var targetMethod = context.GetTargetFunctionMethod();
        if (HasAllowAnonymousAttribute(targetMethod))
        {
            await next(context);
            return;
        }
        if (HasWorkerAuthorizeAttribute(targetMethod))
        {
            var claims = context.Features.Get<JwtPrincipalFeature>()?.Principal.Claims;
            var roleClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (roleClaim != "CoffeeClubWorker")
            {
                var req = await context.GetHttpRequestDataAsync();
                var res = req.CreateResponse();
                res.StatusCode = HttpStatusCode.Forbidden;
                context.GetInvocationResult().Value = res;
                return;
            }
        }
        await next(context);
    }


    private bool HasWorkerAuthorizeAttribute(MethodInfo targetMethod)
    {
        var attributes = GetCustomAttributesOnClassAndMethod<WorkerAuthorizeAttribute>(targetMethod);
        return attributes.Any();
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
