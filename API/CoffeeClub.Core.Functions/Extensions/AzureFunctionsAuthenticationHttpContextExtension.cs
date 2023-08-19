using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeClub_Core_Functions.Extensions;

public static class AzureFunctionsAuthenticationHttpContextExtension
{
    public static async Task<(bool, IActionResult?)> AuthenticateAzureFunctionAsync(this HttpContext httpContext)
    {
        if (httpContext == null)
        {
            throw new ArgumentNullException("Parameter httpContext cannot be null");
        }
        AuthenticateResult? result =
        await httpContext.AuthenticateAsync("Bearer").ConfigureAwait(false);
        if (result.Succeeded)
        {
            httpContext.User = result.Principal;
            return (true, null);
        }
        else
        {
            return (false, new UnauthorizedObjectResult(new ProblemDetails
            {
                Title = "Authorization failed.",
                Detail = result.Failure?.Message,
            }));
        }
    }
}