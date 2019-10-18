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

            ReceiveAsync<DeliveryGoods>( async msg =>
            {
                var getTransportData = new Api.GoodsData(100, 200, 300, 400, "description");
                var transport = ApplicationActorsSystem.Instance.TransportActorLink;
                var transportInfo = (Api.TransportData) await ApplicationActorsSystem.Instance.TransportActorInstance.Ask(getTransportData);
                var result = await _deliveryService.DeliverGoodsAsync(msg, transportInfo);
                Sender.Tell(result);
                Sender.Tell(new DeliveryResult(TransportType.Plain,"ShipId", DateTime.Now, "address", true));
            });
        }

        public static Props Props(IDeliveryService deliveryService)
        {
            return Akka.Actor.Props.Create(() => new DeliveryActor(deliveryService));
        }
    }
}
