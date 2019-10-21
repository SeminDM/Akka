using Akka.Actor;
using Akka.Configuration;
using Api;
using DeliveryApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Actors;

namespace DeliveryActors
{
    public class ApplicationActorsSystem
    {
        private static volatile ApplicationActorsSystem instance;
        private static readonly object instanceLock = new object();

        public static IDeliveryService DeliveryService { get; private set; }
        public static ITransportService TransportService { get; private set; }

        public ActorSystem ActorSystem { get; private set; }
        public ActorSelection TransportActorInstance { get; private set; }
        public IActorRef DeliveryActorInstance { get; private set; }
        public IActorRef TransportActorLink { get; private set; }
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
                        Address address = Address.Parse(DeliveryActorSettings.TransportActorUrlForDeployment);
                        var app = instance = new ApplicationActorsSystem();
                        app.ActorSystem = ActorSystem.Create("DeliverySystem", DeliveryActorSettings.config);
                        app.DeliveryActorInstance = app.ActorSystem.ActorOf(DeliveryActor.Props(DeliveryService), "DeliveryActor");
                        app.TransportActorLink = app.ActorSystem
                          .ActorOf(TransportActor.Props(TransportService)
                          .WithDeploy(Deploy.None.WithScope(new RemoteScope(address)))
                          .WithDispatcher("akka.actor.my-dispatcher")
                          .WithMailbox("akka.actor.custom-mailbox"), 
                          "TransportDeploy");
                        //app.TransportActorInstance = app.ActorSystem.ActorSelection(DeliveryActorSettings.TransportActorUrl);
                        //var k = app.TransportActorInstance.ResolveOne(TimeSpan.FromSeconds(2));
                        app.DeliveryActorInstance.Tell(new DeliveryGoods(new string[] { "asd" }));
                    }
                }
                return instance;
            }
        }
        public ApplicationActorsSystem()
        {

        }

        public ApplicationActorsSystem(IDeliveryService deliveryService, ITransportService transportService)
        {
            DeliveryService = deliveryService;
            TransportService = transportService;
        }
    }
}
