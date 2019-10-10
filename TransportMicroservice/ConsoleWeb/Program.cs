using System;
using Actors;
using Akka.Actor;

namespace ConsoleWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("TransportSystem", TranspotrActorSettings.config))
            {
                var transportActor = system.ActorOf<TransportActor>("TransportActor");
                Console.WriteLine(transportActor.Path);
                Console.ReadKey();
            }
        }
    }
}
