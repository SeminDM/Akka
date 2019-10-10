using Akka.Actor;
using Akka.Configuration;
using DeliveryApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DeliveryActors
{
    public class ApplicationActorsSystem
    {
        private static volatile ApplicationActorsSystem instance;
        private static readonly object instanceLock = new object();
        
        public static IDeliveryService DeliveryService { get; private set; }

        public ActorSystem ActorSystem { get; private set; }
        public ActorSelection TransportActorInstance { get; private set; }
        public IActorRef DeliveryActorInstance { get; private set; }
        public string SystemNum = "1";
        public static ApplicationActorsSystem Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        var app = instance = new ApplicationActorsSystem();
                        app.ActorSystem = ActorSystem.Create("DeliverySystem", DeliveryActorSettings.config);
                        app.DeliveryActorInstance = app.ActorSystem.ActorOf(DeliveryActor.Props(DeliveryService), "DeliveryActor");
                        app.TransportActorInstance = app.ActorSystem.ActorSelection(DeliveryActorSettings.TransportActorUrl);
                    }
                }
                return instance;
            }
        }
        public ApplicationActorsSystem()
        {

        }

        public ApplicationActorsSystem(IDeliveryService deliveryService)
        {
            DeliveryService = deliveryService;
        }
    }
}
