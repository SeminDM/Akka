using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace AkkaShop.Hubs
{
    public class DeliveryHub : Hub
    {
        private static IHubCallerClients Client;
        public async Task SendMessageAsync(string message)
        {
            if (Clients != null)
                Client = Clients;
            if (Client != null)
            {
                await Client.All.SendAsync("SendMessage", message);
            }
        }
    }
}