using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AkkaShop.Models;
using Akka.Actor;
using NotificationApi;
using DeliveryApi;
using AkkaShop.Hubs;
using System.Threading;

namespace AkkaShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IActorRef _notificationActor;
        private readonly IActorRef _deliveryActor;
        private readonly DeliveryHub _deliveryHub;

        public HomeController(DeliveryActorProvider deliveryActorProvider, NotificationActorProvider notificationActorProvider, DeliveryHub deliveryHub)
        {
            _deliveryActor = deliveryActorProvider();
            _notificationActor = notificationActorProvider();
            _deliveryHub = deliveryHub;
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

        [HttpGet]
        public IActionResult DeliverAsync()
        {
            return  View();
        }

        [HttpPost]
        public async Task DeliverAsync(string goods)
        {
            var rand = new Random();

            foreach (var good in goods.Split(','))
            {
                var randomNumber = rand.Next(1, 1000);

                // notify about delivery start
                var startDeliveryNotification = new DeliveryStartNotification(good);
                _notificationActor.Tell(startDeliveryNotification);
                // deliver goods
                var deliveryData = new DeliveryGoods( new string[] { good });
                var result = await _deliveryActor.Ask<DeliveryResult>(deliveryData);
                // notify about delivery finish
                var delivaryFinishNotification = new DeliveryFinishNotification(good, result.ShipId, 
                    (NotificationApi.TransportType)result.TransportType, result.DeliveryDate, result.IsSuccess);

                _notificationActor.Tell(delivaryFinishNotification);

                var msg = result.IsSuccess
                ? $"{good} is delivered to {result.Address} by {result.TransportType} {result.ShipId} at {result.DeliveryDate} successfully"
                : $"{good} delivery to {result.Address} by {result.TransportType} {result.ShipId} at {result.DeliveryDate} was failed";

                await _deliveryHub.SendMessageAsync(msg);
            }
        }

    }
}
