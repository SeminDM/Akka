using Akka.Actor;
using DeliveryApi;
using DeliveryCore;
using System;

namespace DeliveryActors
{
    public class DeliveryActor : ReceiveActor
    {
        private readonly IDeliveryService _service;
        private readonly ActorSelection _server = Context.ActorSelection("akka.tcp://TransportSystem@localhost:8081/user/TransportActor");

        public DeliveryActor()
        {
            _service = new DeliveryService();

            Receive<DeliveryGoods>(msg =>
            {
                var getTransportData = new Api.GoodsData
                {
                    Height = 100,
                    Length = 200,
                    Weight = 300,
                    Width = 400
                };
                var transportInfo = (Api.TransportData) _server.Ask(getTransportData).Result;

                var result = _service.DeliverGoods(msg, transportInfo);
                Sender.Tell(result);
            });
        }
    }
}
