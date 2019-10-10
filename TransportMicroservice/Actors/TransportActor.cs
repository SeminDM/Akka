using Api;
using Core;
using Akka.Actor;

namespace Actors
{
    public class TransportActor : ReceiveActor
    {
        private readonly ITransportService _service;

        public TransportActor()
        {
            _service = new TransportService();

            Receive<GoodsData>(msg =>
            {
                var result = _service.GetTransportInfo(msg);
                
                Sender.Tell(result);
            });
        }
    }

}
