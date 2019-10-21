﻿using Api;
using Core;
using Akka.Actor;
using System.Diagnostics;
using System.IO;
using System;
using System.Threading;

namespace Actors
{
    
    public class TransportActor : ReceiveActor
    {
        private static int counter = 0;
        private Stopwatch Stopwatch = new Stopwatch();
        private static string path = @"C:\Temporary\FromPerfomaceTestResultTransport.txt";

        public TransportActor()
        {
            Receive<GoodsData>(_ =>
            {
                Stopwatch.Start();
                   counter++;
                if (counter % 10000 == 0)
                {
                    Console.WriteLine(counter);
                }
                if (counter == 200_000)
                {
                    Stopwatch.Stop();

                    using (var sw = new StreamWriter(path, true))
                    {
                        sw.WriteLine($"min:{Stopwatch.Elapsed.Minutes} s:{Stopwatch.Elapsed.Seconds} ms:{Stopwatch.Elapsed.Milliseconds} counter:{counter}");
                        sw.WriteLine($"msg per sec:{counter/(Stopwatch.Elapsed.Minutes * 60 + Stopwatch.Elapsed.Seconds)}");
                    }
                }

            }); 
        }
    }
}