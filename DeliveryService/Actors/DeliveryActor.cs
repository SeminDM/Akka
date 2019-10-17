using Akka.Actor;
using DeliveryApi;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace DeliveryActors
{
    public class DeliveryActor : ReceiveActor
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveryActor(IDeliveryService deliveryService)
        {
           
            _deliveryService = deliveryService;

            Receive<DeliveryGoods>( msg =>
            {
                string messagePerfomanceTest = File.ReadAllText(@"C:\Temporary\FromPerfomaceTest.txt");

                var getTransportData = new Api.GoodsData(100, 200, 300, 400, messagePerfomanceTest);
                Stopwatch s = new Stopwatch();
                s.Start();
                var transport = ApplicationActorsSystem.Instance.TransportActorLink;
                for (int i = 0; i < 1_000_000; i++)
                {
                    transport.Tell(getTransportData);
                }
                s.Stop();
                using (StreamWriter sw = new StreamWriter(@"C:\Temporary\LogFromDelivery.txt", true))
                {
                    sw.WriteLine($"s: {s.Elapsed.Seconds} ms: {s.Elapsed.Milliseconds}");
                }

                //var transportInfo = (Api.TransportData)await ApplicationActorsSystem.Instance.TransportActorInstance.Ask(getTransportData);
                //var result = await _deliveryService.DeliverGoodsAsync(msg, transportInfo);
                //Sender.Tell(result);
                //Sender.Tell(new DeliveryResult(TransportType.Plain,"ShipId", DateTime.Now, "address", true));
            });
        }

        public static Props Props(IDeliveryService deliveryService)
        {
            return Akka.Actor.Props.Create(() => new DeliveryActor(deliveryService));
        }

    }
}
