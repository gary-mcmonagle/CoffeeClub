using System.Net;
using CoffeeClub_Core_Functions.CustomConfiguration;
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
        var tokenGot = context.TryGetTokenFromHeaders(out var token);
        if (tokenGot)
        {
            try
            {
                var principle = _tokenValidator.ValidateToken(token, new TokenValidationParameters(), out _);
                context.Features.Set(new JwtPrincipalFeature(principle, token));
                await next(context);
            }
            catch (Exception)
            {
                await SetAsUnauth(context);
            }
        }
        else
        {
            await SetAsUnauth(context);
        }
    }

    private async Task SetAsUnauth(FunctionContext context)
    {
        var req = await context.GetHttpRequestDataAsync();
        var res = req.CreateResponse();
        res.StatusCode = HttpStatusCode.Unauthorized;
        context.GetInvocationResult().Value = res;
    }
}
