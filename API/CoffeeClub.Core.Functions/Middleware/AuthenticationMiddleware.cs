using System.Net;
using System.Reflection;
using CoffeeClub_Core_Functions.CustomConfiguration;
using CoffeeClub_Core_Functions.CustomConfiguration.Authorization;
using CoffeeClub_Core_Functions.Extensions;
using Google.Apis.Auth;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeClub_Core_Functions.Middleware;

public class AuthenticationMiddleware : IFunctionsWorkerMiddleware
{
    private readonly ISecurityTokenValidator _tokenValidator;

    public AuthenticationMiddleware()
    {
        _tokenValidator = new GoogleTokenValidator();
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        if (HasAllowAnonymousAttribute(context.GetTargetFunctionMethod()))
        {
            await next(context);
            return;
        }

        var tokenGot = context.TryGetTokenFromHeaders(out var token);
        if (tokenGot)
        {
            try
            {
                var principle = _tokenValidator.ValidateToken(token, new TokenValidationParameters(), out _);
                context.Features.Set(new JwtPrincipalFeature(principle, token));
                await next(context);
            }
            catch (SecurityTokenException)
            {
                await SetAsUnauth(context);
            }
        }
        else
        {
            await SetAsUnauth(context);
        }
    }
    private bool HasAllowAnonymousAttribute(MethodInfo targetMethod)
    {
        var attributes = GetCustomAttributesOnClassAndMethod<AllowAnonymousAttribute>(targetMethod);
        return attributes.Any();
    }


    private async Task SetAsUnauth(FunctionContext context)
    {
        var req = await context.GetHttpRequestDataAsync();
        var res = req.CreateResponse();
        res.StatusCode = HttpStatusCode.Unauthorized;
        context.GetInvocationResult().Value = res;
    }

    private static List<T> GetCustomAttributesOnClassAndMethod<T>(MethodInfo targetMethod)
where T : Attribute
    {
        var methodAttributes = targetMethod.GetCustomAttributes<T>();
        var classAttributes = targetMethod.DeclaringType.GetCustomAttributes<T>();
        return methodAttributes.Concat(classAttributes).ToList();
    }
}
