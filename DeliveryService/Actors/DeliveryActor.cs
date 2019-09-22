using Akka.Actor;
using DeliveryApi;
using DeliveryCore;

namespace DeliveryActors
{
    public class DeliveryActor : ReceiveActor
    {
        private readonly IDeliveryService _service;

        public DeliveryActor()
        {
            _service = new DeliveryService();

            Receive<DeliveryData>(msg =>
            {
                var result = _service.DeliverGoods(msg);
                Sender.Tell(result);
            });
        }
    }
}
