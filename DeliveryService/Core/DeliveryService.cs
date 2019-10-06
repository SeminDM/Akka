using DeliveryApi;
using System;
using System.IO;
using System.Threading;

namespace DeliveryCore
{
    public class DeliveryService : IDeliveryService
    {
        public DeliveryResult DeliverGoods(DeliveryGoods data, Api.TransportData transportInfo)
        {
            var rand = new Random();
            Thread.Sleep(TimeSpan.FromSeconds(5));
            var now = DateTime.Now;
            var path = @"E:\delivery.txt";
            var randomNumber = rand.Next(1, 100);
            var success = randomNumber % 2 == 0;

            var msg = success
                ? $"Goods {string.Join(",", data.Goods)} are delivered by {transportInfo.TransportType} {transportInfo.VehicleNumber} at {now}"
                : $"Delivery was failed by {transportInfo.TransportType} {transportInfo.VehicleNumber} at {now}";

            File.AppendAllLines(path, new[] { msg });

            return new DeliveryResult
            {
                Address = $"Baker street'{randomNumber}",
                DeliveryDate = now,
                IsSuccess = success,
                ShipId = transportInfo.VehicleNumber.ToString(),
                TransportType = (TransportType)transportInfo.TransportType
            };
        }
    }
}
