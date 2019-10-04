using Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Core
{
    public class TransportService : ITransportService
    {
        Random rnd = new Random();

        public TransportData GetTransportInfo(GoodsData goodsData)
        {
            var secondsCount = rnd.Next(0, 5);
            Thread.Sleep(TimeSpan.FromSeconds(secondsCount));
            return new TransportData { VehicleNumber = rnd.Next(10000, 50000), DepartureDate = RandomDate() };
        }

        public DateTime RandomDate()
        {
            DateTime startdate = new DateTime(2014, 1, 1, 7, 0, 0);
            return startdate.AddMinutes(rnd.Next(Int32.MaxValue)).ToUniversalTime();
        }
    }
}
