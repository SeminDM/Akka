using Api;
using Core;
using Akka.Actor;
using System.Diagnostics;
using System.IO;
using System;

namespace Actors
{
    public class TransportActor : ReceiveActor
    {
        private readonly ITransportService _service;

        public TransportActor(ITransportService service)
        {
            _service = service;

            ReceiveAsync<GoodsData>(async msg =>
            {
                var result = await _service.GetTransportInfoAsync(msg);
                Sender.Tell(result);
            });
        }

        public static Props Props(ITransportService transportService)
        {
            return Akka.Actor.Props.Create(() => new TransportActor(transportService));
        }
    }
}