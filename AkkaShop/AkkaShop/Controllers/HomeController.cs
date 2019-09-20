using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AkkaShop.Models;
using Akka.Actor;
using NotificationService;
using DeliveryService.Actors;
using DeliveryService;

namespace AkkaShop.Controllers
{
    public class HomeController : Controller
    {
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

        public IActionResult Deliver(string goods)
        {
            var system = ActorSystem.Create("AkkaShop");
            IActorRef notificationActor = system.ActorOf<NotificationActor>("NotificationActor");
            IActorRef deliveryActor = system.ActorOf<DeliveryActor>("DeliveryActor");

            var rand = new Random();
            var randomNumber = rand.Next(1, 1000);

            // notify about delivery start
            var startDeliveryNotification = new DelivaryStartNotification
            {
                ShipId = randomNumber.ToString(),
                TransportType = (NotificationService.TransportType)(randomNumber % 4)
            };
            notificationActor.Tell(startDeliveryNotification);

            // deliver goods
            var deliveryData = new DeliveryData
            {
                Goods = goods.Split(','),
                ShipId = randomNumber.ToString(),
                TransportType = (DeliveryService.TransportType)(randomNumber % 4)
            };
            var result = deliveryActor.Ask<DeliveryResult>(deliveryData).Result;
            
            // notify about delivery finish
            var delivaryFinishNotification = new DelivaryFinishNotification
            {
                ShipId = randomNumber.ToString(),
                DeliveryDate = result.DeliveryDate,
                IsSuccess = result.IsSuccess
            };
            notificationActor.Tell(delivaryFinishNotification);

            return Json(result);
        }
    }
}
