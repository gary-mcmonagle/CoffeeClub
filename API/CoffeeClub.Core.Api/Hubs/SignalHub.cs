using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CoffeeClub.Core.Api.Hubs;

public class SignalrHub : Hub
{
    public async Task NewMessage(NotifyMessage message)
    {
        var user = Context.User;
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}

public record NotifyMessage
{
    public string Message { get; set; } = string.Empty;
}