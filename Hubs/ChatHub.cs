using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
namespace TDTU_Chat.Hubs;
public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        var timestamp = DateTime.UtcNow.ToString("HH:mm:ss");
        await Clients.All.SendAsync("ReceiveMessage", user, message, timestamp);
    }
}

