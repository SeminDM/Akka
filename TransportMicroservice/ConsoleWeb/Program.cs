using System;
using System.Threading;
using System.Threading.Tasks;
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
                var transportActor = system.ActorOf(Props.Create<TransportActor>()
                    .WithDispatcher("akka.actor.my-dispatcher")
                    .WithMailbox("akka.actor.custom-mailbox"), "TransportActor");
                

                Console.WriteLine(transportActor.Path);
                Console.ReadKey();
            }
        }
    }
}
