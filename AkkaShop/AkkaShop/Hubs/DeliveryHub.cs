using Akka.Actor;
using AkkaShop.Core;
using DeliveryApi;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AkkaShop.Hubs
{
    public class DeliveryHub : Hub
    {
        private static NotifyHub _notifyHub = new NotifyHub();
        private static Deliver _deliver;
        private static Random r = new Random();

        public void GetStarted()
        {
            if (_notifyHub.IsHaveClinet == false)
                _notifyHub.SetClient(Clients);
            if (_notifyHub.IsHaveClinet == true)
                NotifyHub.Client.All.SendAsync("ViewDeliveryResult", "GettingStart");
        }

        public void SetDeliver(Deliver deliver)
        {
            _deliver = deliver;
        }

        public async void StartDelivery()
        {
            await _deliver.StartDelivery();
            this.NotifyClients("All goods was delivered");
        }

        public async void SetGoodsAndStartDelivery(string goods)
        {
            var goodsArray = goods.Split(',');

            foreach (var g in goodsArray)
            {
                var rNumber = r.Next(1, 1000);
                var data = new DeliveryData()
                {
                    Goods = new string[] { g },
                    ShipId = rNumber.ToString(),
                    TransportType = (DeliveryApi.TransportType)(rNumber % 4)
                };
                this.NotifyClients($"{JsonConvert.SerializeObject(data)}, start delivering");

                var result = await _deliver.StartDeliveryOneGood(data);
                this.NotifyClients($"{JsonConvert.SerializeObject(result)}, was delivered");
            }
        }

        public void NotifyClients(string message)
        {

            if (_notifyHub.IsHaveClinet == true)
                NotifyHub.Client.All.SendAsync("SendMessage", message);
        }
    }
}
