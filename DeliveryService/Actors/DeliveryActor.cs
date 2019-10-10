﻿using Akka.Actor;
using DeliveryApi;
using DeliveryCore;
using System;

namespace DeliveryActors
{
    public class DeliveryActor : ReceiveActor
    {
        private readonly IDeliveryService _service;
        private readonly ActorSelection _server = Context.ActorSelection(DeliveryActorSettings.TransportActorUrl);

        public DeliveryActor()
        {

            _service = new DeliveryService();

            Receive<DeliveryGoods>(msg =>
            {
                var getTransportData = new Api.GoodsData(100, 200, 300, 400);

                var transportInfo = (Api.TransportData) _server.Ask(getTransportData).Result;
                
                var result = _service.DeliverGoods(msg, transportInfo);
                
                Sender.Tell(result);
            });
        }
    }
}
