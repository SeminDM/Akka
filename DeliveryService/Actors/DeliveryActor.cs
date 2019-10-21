using Akka.Actor;
using DeliveryApi;
using System;
using System.Diagnostics;
using System.IO;

namespace DeliveryActors
{
    public class DeliveryActor : ReceiveActor
    {
        public DeliveryActor(IDeliveryService deliveryService)
        {

            var messagePerformanceTest = File.ReadAllText(@"C:\Temporary\FromPerfomaceTestMessage.txt");//10kb
          
            Receive<DeliveryGoods>( msg =>
            {
                var path = @"C:\Temporary\FromPerfomaceTestResultDelivery.txt";
                var getTransportData = new Api.GoodsData(100, 200, 300, 400, "description" );
                var transport = ApplicationActorsSystem.Instance.TransportActorLink;
                for (int i = 0; i <= 200_000; i++)
                {
                    //ApplicationActorsSystem.Instance.TransportActorInstance.Tell(getTransportData);
                    ApplicationActorsSystem.Instance.TransportActorLink.Tell(getTransportData);
                }
            });
        }

        public static Props Props(IDeliveryService deliveryService)
        {
            return Akka.Actor.Props.Create(() => new DeliveryActor(deliveryService));
        }
    }
}
