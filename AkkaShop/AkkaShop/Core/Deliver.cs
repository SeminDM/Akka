using Akka.Actor;
using DeliveryApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AkkaShop.Core
{
    public class Deliver
    {
        private static List<DeliveryData> _goods = new List<DeliveryData>();
        private static List<DeliveryResult> _result = new List<DeliveryResult>();
        private static Random r = new Random();
        private static int rNumber = r.Next(1, 1000);
        private static IActorRef _deliver;

        public void SetActor(IActorRef Deliver)
        {
            _deliver = Deliver;
        }

        public void SetGood(string good)
        {
            if (good != null)
            {
                if (good.Contains(','))
                {
                    var temp = good.Split(',');
                    foreach (var g in temp)
                        _goods.Add(new DeliveryData
                        {
                            Goods = good.Split(','),
                            ShipId = rNumber.ToString(),
                            TransportType = (DeliveryApi.TransportType)(rNumber % 4)
                        });
                }
                else
                    _goods.Add(new DeliveryData
                    {
                        Goods = new string[] { good },
                        ShipId = rNumber.ToString(),
                        TransportType = (DeliveryApi.TransportType)(rNumber % 4)
                    });
            }
        }

        public void SetGoods(string[] goods)
        {
            if (goods != null)
                _goods.Add(new DeliveryData
                {
                    Goods = goods,
                    ShipId = rNumber.ToString(),
                    TransportType = (DeliveryApi.TransportType)(rNumber % 4)
                });
        }

        public async Task<List<DeliveryResult>> StartDelivery()
        {
            foreach (var delData in _goods)
            {
                var tempDeliveryResult = await _deliver.Ask<DeliveryResult>(delData);
                _result.Add(tempDeliveryResult);
            }
            return _result;
        }

        public async Task<DeliveryResult> StartDeliveryOneGood(DeliveryData data)
        {
            return await _deliver.Ask<DeliveryResult>(data);
        }

        public List<DeliveryData> GetGoods()
        {
            return _goods;
        }

        private string ToImitateWorking()
        {
            int secondsCount = r.Next(0, 4);
            Thread.Sleep(TimeSpan.FromSeconds(secondsCount));
            return secondsCount.ToString();
        }

    }
}
