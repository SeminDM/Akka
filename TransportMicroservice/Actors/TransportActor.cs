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

            ReceiveAsync<GoodsData>(async msg =>
            {
                var result = await _service.GetTransportInfoAsync(msg);
                
                Sender.Tell(result);
            });
        }
    }

}
