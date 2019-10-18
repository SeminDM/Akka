using Akka.Configuration;
using DeliveryApi;
using System;
using System.IO;
using System.Reflection;

namespace DeliveryActors
{
    public static class DeliveryActorSettings
    {
        public static readonly Config config = ConfigurationFactory.ParseString(File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Settings/deliveryActor.hocon")));

        public static readonly string TransportActorUrl = Configurator.GetValue<string>("TransportActorUrl");
        public static readonly string TransportActorUrlForDeployment = Configurator.GetValue<string>("TransportActorUrlForDeployment");
    }
}
