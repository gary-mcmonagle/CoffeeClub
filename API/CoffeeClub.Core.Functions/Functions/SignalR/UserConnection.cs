namespace CoffeeClub_Core_Functions.Functions.SignalR;

public class UserConnection
{
    private IUserRepository _userRepository;

    public UserConnection(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [Function(nameof(OnUserConnected))]
    [SignalROutput(HubName = "serverless")]
    public async Task<SignalRGroupAction> OnUserConnected(
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
    [SignalROutput(HubName = Constants.HubName)]
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
