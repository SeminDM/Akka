using Akka.Actor;
using Akka.Configuration;
using Api;
using DeliveryApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Autofac;
using Actors;

namespace DeliveryActors
{
    public class ApplicationActorsSystem
    {
        private static volatile ApplicationActorsSystem instance;
        private static readonly object instanceLock = new object();
        private static ILifetimeScope lifetimeScope;

        public IDeliveryService DeliveryService { get; private set; }
        public ITransportService TransportService { get; private set; }

        public ActorSystem ActorSystem { get; private set; }

        public IActorRef DeliveryActorInstance { get; private set; }
        //public ActorSelection TransportActorInstance { get; private set; }
        public IActorRef TransportActorInstance { get; private set; }

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
                        app.DeliveryService = lifetimeScope.Resolve<IDeliveryService>();
                        app.TransportService = lifetimeScope.Resolve<ITransportService>();

                        app.ActorSystem = ActorSystem.Create("DeliverySystem", DeliveryActorSettings.config);
                        app.DeliveryActorInstance = app.ActorSystem.ActorOf(DeliveryActor.Props(app.DeliveryService), "DeliveryActor");
                        Address address = Address.Parse(DeliveryActorSettings.TransportActorUrlForDeployment);
                        app.TransportActorInstance = app.ActorSystem.ActorOf(TransportActor.Props(app.TransportService)
                           .WithDeploy(Deploy.None.WithScope(new RemoteScope(address))), "TransportDeploy");
                        //app.TransportActorInstance = app.ActorSystem.ActorSelection(DeliveryActorSettings.TransportActorUrl);
                        //var k = app.TransportActorInstance.ResolveOne(TimeSpan.FromSeconds(2));
                    }
                }
                return instance;
            }
        }
        public ApplicationActorsSystem()
        {

        }

        public ApplicationActorsSystem(ILifetimeScope scope)
        {
            lifetimeScope = scope;
        }
    }
}
