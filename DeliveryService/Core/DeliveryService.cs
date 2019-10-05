using DeliveryApi;
using System;
using System.IO;
using System.Threading;

namespace DeliveryCore
{
    public class DeliveryService : IDeliveryService
    {
        public DeliveryResult DeliverGoods(DeliveryData data)
        {
            var rand = new Random();
            var secondsCount = rand.Next(0, 5);
            Thread.Sleep(TimeSpan.FromSeconds(secondsCount));
            var now = DateTime.Now;
            var path = @"delivery.txt";
            var randomNumber = rand.Next(1, 100);
            var success = randomNumber % 2 == 0;
            var waitedTime = $"you was waited {secondsCount} days";

            var msg = success
                ? $"Goods {string.Join(",", data.Goods)} are delivered by {data.ShipId} at {now}, {waitedTime}"
                : $"Delivery was failed at {now}, {waitedTime}";

            File.AppendAllLines(path, new[] { msg });

            return new DeliveryResult { Address = $"{randomNumber} Baker street", DeliveryDate = now, IsSuccess = success };
        }
    }
}
