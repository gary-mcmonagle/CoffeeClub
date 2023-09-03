using Microsoft.Azure.Functions.Worker.Middleware;

namespace CoffeeClub_Core_Functions.Middleware;

public class IdentityMiddleware : IFunctionsWorkerMiddleware
{
    private IUserRepository _userRepository;

    public IdentityMiddleware(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var claims = context.Features.Get<JwtPrincipalFeature>()?.Principal.Claims;
        var subClaim = claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        var roleClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        var userId = await _userRepository.GetUserId(subClaim!, AuthProvider.Google, roleClaim == "CoffeeClubWorker");
        context.Features.Set(userId);
        await next(context);
    }
}
