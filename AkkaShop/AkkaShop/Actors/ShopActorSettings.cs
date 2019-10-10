using Akka.Configuration;
using System.IO;

namespace AkkaShop
{
    public static class ShopActorSettings
    {
        public static readonly Config config = ConfigurationFactory.ParseString
            (File.ReadAllText(Directory.GetCurrentDirectory() +"/Actors/shopActor.hocon"));

        public static readonly string DeliveryActorUrl = Configurator.GetValue<string>("DeliveryActorUrl");
    }
}
