using CoffeeClub_Core_Functions.CustomConfiguration.Authorization;

namespace CoffeeClub_Core_Functions;

public class OnUserConnections
{
    private IUserRepository _userRepository;

    public OnUserConnections(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [Function(nameof(OnUserConnected))]
    [SignalROutput(HubName = "serverless")]
    [AllowAnonymous]
    public async Task <SignalRGroupAction> OnUserConnected(
    [SignalRTrigger("serverless", "connections", "connected")]
        SignalRInvocationContext invocationContext, FunctionContext functionContext)
    {
        var user = await _userRepository.GetAsync(Guid.Parse(invocationContext.UserId));
        return new SignalRGroupAction(SignalRGroupActionType.Add)
        {
            GroupName = user.IsWorker ? "workers" : "clients",
            UserId = invocationContext.UserId
        };
    }


    [Function(nameof(OnUserDisconnected))]
    [SignalROutput(HubName = "serverless")]
    [AllowAnonymous]
    public async Task<SignalRGroupAction> OnUserDisconnected(
    [SignalRTrigger("serverless", "connections", "disconnected")]
        SignalRInvocationContext invocationContext, FunctionContext functionContext)
    {
        var user = await _userRepository.GetAsync(Guid.Parse(invocationContext.UserId));
        return new SignalRGroupAction(SignalRGroupActionType.Remove)
        {
            GroupName = user.IsWorker ? "workers" : "clients",
            UserId = invocationContext.UserId
        };
    }
}
