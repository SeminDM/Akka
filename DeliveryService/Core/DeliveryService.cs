using DeliveryApi;
using System;
using System.IO;

namespace DeliveryCore
{
    public class DeliveryService : IDeliveryService
    {
        public DeliveryResult DeliverGoods(DeliveryData data)
        {
            var rand = new Random();
            var now = DateTime.Now;
            var path = @"E:\delivery.txt";
            var randomNumber = rand.Next(1, 100);
            var success = randomNumber % 2 == 0;

            var msg = success
                ? $"Goods {string.Join(",", data.Goods)} are delivered by {data.ShipId} at {now}"
                : $"Delivery was failed at {now}";

            File.WriteAllText(path, msg);
            return new DeliveryResult { Address = $"{randomNumber} Baker street", DeliveryDate = now, IsSuccess = success };
        }
    }
}
