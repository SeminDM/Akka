using Akka.Configuration;
using System.IO;

namespace Actors
{
    public static class TranspotrActorSettings
    {
        public static readonly Config config = ConfigurationFactory.ParseString(File.ReadAllText("Settings/transportActor.hocon"));
    }
}
