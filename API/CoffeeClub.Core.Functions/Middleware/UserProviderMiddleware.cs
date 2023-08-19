using CoffeeClub.Domain.Enumerations;
using CoffeeClub.Domain.Repositories;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace CoffeeClub_Core_Functions.Middleware;

public class UserProviderMiddleware : IFunctionsWorkerMiddleware
{
    private readonly IUserRepository _userRepository;

    public UserProviderMiddleware(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var claims = context.Features.Get<JwtPrincipalFeature>()?.Principal.Claims;
        var subClaim = claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        var user = await _userRepository.GetAsync(subClaim!, AuthProvider.Google);
        context.Features.Set(user);
        await next(context);
    }
}
