using Microsoft.AspNetCore.SignalR;

namespace CoffeeClub.Core.Api.Services;

public class HubUserConnectionProviderService<T> : IHubUserConnectionProviderService<T> where T : Hub
{
    private readonly List<(Guid, string, bool)> _connections = new();
    public void AddConnectionForUser(Guid userId, string connectionId, bool isWorker)
    {
        _connections.Add((userId, connectionId, isWorker));
    }

    public IEnumerable<string> GetConnectionsForUserAsync(Guid userId)
    {
        return _connections.Where(x => x.Item1 == userId).Select(x => x.Item2);
    }

    public IEnumerable<string> GetAllWorkerConnections()
    {
        return _connections.Where(x => x.Item3).Select(x => x.Item2);
    }

    public IEnumerable<string> GetAllWorkerConnections(IEnumerable<Guid> excludeIds)
    {
        return _connections.Where(x => x.Item3 && !excludeIds.Contains(x.Item1)).Select(x => x.Item2);
    }

    public void RemoveConnection(string connectionId)
    {
        _connections.RemoveAll(x => x.Item2 == connectionId);
    }
}

