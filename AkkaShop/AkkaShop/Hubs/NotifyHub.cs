using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AkkaShop.Hubs
{
    public class NotifyHub : Hub
    {
        private static List<string> goods = new List<string>();
        private static List<string> _lastGoods = new List<string>();
        public static IHubCallerClients Client { get; private set; }
        public bool IsHaveClinet { get { return Client != null ? true : false; } private set { } }
        private static string message = "";

        public void SetClient(IHubCallerClients client)
        {
            Client = client;
        }

        public void GetStarted()
        {
            if (Client == null)
                Client = Clients;
            if (Client != null)
                Task.Run(() => Client.All.SendAsync("Notify", "Connected"));

        }

        public void SendMessage(string message = null)
        {
            if (Client != null && message == null)
                Client.All.SendAsync("Notify", "Some message");
            else if (Client != null)
                Client.All.SendAsync("Notify", message);
        }
        
    }
}
