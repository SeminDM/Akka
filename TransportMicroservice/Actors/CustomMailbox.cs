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
            var messString = message as string;
            int messInt; 
            Int32.TryParse(message as string,out messInt);
            if (messString != null)
            {
                return 0;
            }
            if (messInt > 1)
            {
                return 5;
            }
            return 10;
        }
    }
}
