using Akka.Actor;
using DeliveryApi;
using System;
using System.Diagnostics;
using System.IO;

namespace DeliveryActors
{
    public class DeliveryActor : ReceiveActor
    {
        private readonly IDeliveryService _deliveryService;
        private Stopwatch Stopwatch = new Stopwatch();
        public DeliveryActor(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;

            var messagePerformanceTest = File.ReadAllText(@"C:\Temporary\FromPerfomaceTestMessage.txt");//10kb

            //ReceiveAsync<DeliveryGoods>( async msg =>
            //{
            //    var getTransportData = new Api.GoodsData(100, 200, 300, 400, "description");
            //    var transport = ApplicationActorsSystem.Instance.TransportActorLink;
            //    var transportInfo = (Api.TransportData) await ApplicationActorsSystem.Instance.TransportActorInstance.Ask(getTransportData);
            //    var result = await _deliveryService.DeliverGoodsAsync(msg, transportInfo);
            //    Sender.Tell(result);
            //    Sender.Tell(new DeliveryResult(TransportType.Plain,"ShipId", DateTime.Now, "address", true));
            //});            
            Receive<DeliveryGoods>( msg =>
            {
                var path = @"C:\Temporary\FromPerfomaceTestResultDelivery.txt";
                var getTransportData = new Api.GoodsData(100, 200, 300, 400, "description" );
                var transport = ApplicationActorsSystem.Instance.TransportActorLink;
                Stopwatch.Start();
                for (int i = 0; i <= 200_000; i++)
                {
                    ApplicationActorsSystem.Instance.TransportActorInstance.Tell(getTransportData);
                }
                Stopwatch.Stop();
                using (var sw = new StreamWriter(path, true))
                {
                    sw.WriteLine($"min:{Stopwatch.Elapsed.Minutes} s:{Stopwatch.Elapsed.Seconds} ms:{Stopwatch.Elapsed.Milliseconds}");
                }
            });
        }

        public static Props Props(IDeliveryService deliveryService)
        {
            return Akka.Actor.Props.Create(() => new DeliveryActor(deliveryService));
        }
    }
}
