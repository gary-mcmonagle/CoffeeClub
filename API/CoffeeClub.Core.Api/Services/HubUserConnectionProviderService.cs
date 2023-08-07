using Microsoft.AspNetCore.SignalR;

namespace CoffeeClub.Core.Api.Services;

public class HubUserConnectionProviderService<T> : IHubUserConnectionProviderService<T> where T : Hub
{
    private readonly List<(Guid, string)> _connections = new();
    public void AddConnectionForUser(Guid userId, string connectionId)
    {
        _connections.Add((userId, connectionId));
    }

    public IEnumerable<string> GetConnectionsForUserAsync(Guid userId)
    {
        return _connections.Where(x => x.Item1 == userId).Select(x => x.Item2);
    }

    public void RemoveConnection(string connectionId)
    {
        _connections.RemoveAll(x => x.Item2 == connectionId);
    }
}
