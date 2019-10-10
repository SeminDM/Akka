using DeliveryApi;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DeliveryCore
{
    public class DeliveryService : IDeliveryService
    {
        public async Task<DeliveryResult> DeliverGoods(DeliveryGoods data, Api.TransportData transportInfo)
        {
            return Task.Run(() =>
            {
                var rand = new Random();
                Thread.Sleep(TimeSpan.FromSeconds(5));
                var now = DateTime.Now;
                var path = Configurator.GetValue<string>("ReportPath");
                var randomNumber = rand.Next(1, 100);
                var success = randomNumber % 2 == 0;

                var msg = success
                    ? $"Goods {string.Join(",", data.Goods)} are delivered by {transportInfo.TransportType} {transportInfo.VehicleNumber} at {now}"
                    : $"Delivery was failed by {transportInfo.TransportType} {transportInfo.VehicleNumber} at {now}";

                File.AppendAllLines(path, new[] { msg });

                return new DeliveryResult((TransportType)transportInfo.TransportType, transportInfo.VehicleNumber.ToString(),
                    now, $"Baker street'{randomNumber}", success);
            }).Result;
        }

    }
}
