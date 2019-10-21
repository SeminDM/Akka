using Akka.Actor;
using Akka.Configuration;
using Akka.Dispatch;
using System;

namespace Actors
{
    public class CustomMailbox : UnboundedPriorityMailbox
    {
        public CustomMailbox(Settings settings, Config config) : base(settings, config)
        {
        }

        protected override int PriorityGenerator(object message)
        {
            
        }
    }
}
