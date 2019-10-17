using Api;
using Core;
using Akka.Actor;
using System.Diagnostics;
using System.IO;
using System;

namespace Actors
{
    public class TransportActor : ReceiveActor
    {
        private readonly ITransportService _service;
        private Stopwatch stopWatch;
        private int cnt = 0;

        public TransportActor(ITransportService service)
        {
            _service = service;
            stopWatch = new Stopwatch();

            Receive<GoodsData>(msg =>
            {
                cnt++;

                if (cnt == 1)
                {
                    stopWatch.Restart();
                }
                if (cnt % 10_000 == 0)
                {
                    Console.WriteLine(cnt);
                }
                if (cnt == 1_000_000)
                {
                    stopWatch.Stop();
                    using (StreamWriter sw = new StreamWriter(@"C:/Temporary/timer.txt", true))
                    {
                        sw.WriteLine($"m: {stopWatch.Elapsed.Minutes} s: {stopWatch.Elapsed.Seconds} ms: {stopWatch.Elapsed.Milliseconds}");
                    }
                }
                //var result = _service.GetTransportInfo(msg);
                //Sender.Tell(result);
            });
        }

        public static Props Props(ITransportService transportService)
        {
            return Akka.Actor.Props.Create(() => new TransportActor(transportService));
        }
    }
}