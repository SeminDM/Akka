using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AkkaShop.Models;
using Akka.Actor;
using NotificationApi;
using DeliveryApi;
using AkkaShop.Hubs;
using AkkaShop.Core;

namespace AkkaShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IActorRef _notificationActor;
        private readonly IActorRef _deliveryActor;

        public HomeController(DeliveryActorProvider deliveryActorProvider, NotificationActorProvider notificationActorProvider)
        {
            _deliveryActor = deliveryActorProvider();
            _notificationActor = notificationActorProvider();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> DeliverAsync(string goods)
        {
            var rand = new Random();
            var randomNumber = rand.Next(1, 1000);
            
            goods = $"some good {rand.Next(-100, 100)}, some good {rand.Next(-100, 100)}, some good {rand.Next(-100, 100)}, " +
               $"some good {rand.Next(-100, 100)}, some good {rand.Next(-100, 100)}, some good {rand.Next(-100, 100)}";

            NotifyHub notifyHub = new NotifyHub();

            DeliveryHub deliverHub = new DeliveryHub();
            Deliver deliver = new Deliver();

            deliver.SetActor(_deliveryActor);
            deliver.SetGood(goods);

            deliverHub.SetDeliver(deliver);

            
            // notify about delivery start
            var startDeliveryNotification = new DeliveryStartNotification
            {
                ShipId = randomNumber.ToString(),
                TransportType = (NotificationApi.TransportType)(randomNumber % 4)
            };
            var goodsArray = goods.Split(',');

            foreach (var good in goodsArray)
            {
                var rn = rand.Next(1, 1000);
                var tempDeliveryData = new DeliveryData
                {
                    Goods = new string[] { good },
                    ShipId = rn.ToString(),
                    TransportType = (DeliveryApi.TransportType)(rn % 4)
                };

                var msg = $"Goods will be delivered by {tempDeliveryData.TransportType} {tempDeliveryData.ShipId}";

                var result1 = await deliver.StartDeliveryOneGood(tempDeliveryData);
                msg = result1.IsSuccess
               ? $"Your goods are delivered to {result1.Address} successfully"
               : $"Delivery of your goods was failed";
                notifyHub.SendMessage();
            }





            //TODO notify by signalr with actors
            _notificationActor.Tell(startDeliveryNotification);

            // deliver goods
            var deliveryData = new DeliveryData
            {
                Goods = goods.Split(','),
                ShipId = randomNumber.ToString(),
                TransportType = (DeliveryApi.TransportType)(randomNumber % 4)
            };
            var result = await _deliveryActor.Ask<DeliveryResult>(deliveryData, TimeSpan.FromSeconds(5));

            // notify about delivery finish
            var delivaryFinishNotification = new DeliveryFinishNotification
            {
                ShipId = randomNumber.ToString(),
                DeliveryDate = result.DeliveryDate,
                IsSuccess = result.IsSuccess
            };
            _notificationActor.Tell(delivaryFinishNotification);

            return Json(result);
        }
    }
}
