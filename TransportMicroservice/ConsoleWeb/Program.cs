using System;
using Actors;
using Akka.Actor;
using Akka.Configuration;

namespace ConsoleWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(@"
                    akka {  
                        actor {
                            provider = remote
                        }
                        remote {
                            dot-netty.tcp {
                                port = 8081
                                hostname = 0.0.0.0
                                public-hostname = localhost
                            }
                        }
                    }");

            using (var system = ActorSystem.Create("TransportSystem", config))
            {
                var transportActor = system.ActorOf<TransportActor>("TransportActor");
                Console.WriteLine(transportActor.Path);

                Console.ReadKey();
            }
        }
    }
}
