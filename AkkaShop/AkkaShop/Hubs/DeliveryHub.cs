using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace AkkaShop.Hubs
{
    public class DeliveryHub : Hub
    {
        public async Task SendMessageAsync(string message)
        {
            await Clients.All.SendAsync("SendMessage", message);
        }
    }
}