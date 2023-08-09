using Microsoft.AspNetCore.SignalR;

namespace CoffeeClub.Core.Api.Services;

public interface IHubUserConnectionProviderService<T> where T : Hub
{
    IEnumerable<string> GetConnectionsForUserAsync(Guid userId);
    void AddConnectionForUser(Guid userId, string connectionId, bool isWorker);
    void RemoveConnection(string connectionId);

    IEnumerable<string> GetAllWorkerConnections(IEnumerable<Guid> excludeIds);
    IEnumerable<string> GetAllWorkerConnections();
}
