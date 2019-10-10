using Akka.Actor;
using DeliveryApi;
using System;

namespace DeliveryActors
{
    public class DeliveryActor : ReceiveActor
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveryActor(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;

            Receive<DeliveryGoods>(msg =>
            {
                var getTransportData = new Api.GoodsData(100, 200, 300, 400);

                var transportInfo = (Api.TransportData)ApplicationActorsSystem.Instance.TransportActorInstance.Ask(getTransportData).Result;

                var result = _deliveryService.DeliverGoods(msg, transportInfo);

                Sender.Tell(result);
            });
        }

        public static Props Props(IDeliveryService deliveryService)
        {
            return Akka.Actor.Props.Create(() => new DeliveryActor(deliveryService));
        }

    }
}
