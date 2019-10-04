using Api;
using Core;
using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

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
