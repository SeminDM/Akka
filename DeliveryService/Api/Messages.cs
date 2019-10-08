using System;

namespace DeliveryApi
{
    public class DeliveryResult
    {
        public TransportType TransportType { get; }
        public string ShipId               { get; }
        public DateTime DeliveryDate       { get; }
        public string Address              { get; }
        public bool IsSuccess              { get; set; }

        public DeliveryResult(TransportType TransportType, string ShipId, DateTime DeliveryDate, string Address, bool IsSuccess = false)
        {
            this.TransportType  = TransportType;
            this.ShipId         = ShipId;
            this.DeliveryDate   = DeliveryDate;
            this.Address        = Address;
            this.IsSuccess      = IsSuccess;
        }
    }

    public class DeliveryGoods
    {
        public string[] Goods { get; }

        public DeliveryGoods(string[] Goods)
        {
            this.Goods = Goods;
        }
    }

    public enum TransportType
    {
        Undefined,
        Ship,
        Plain,
        Train
    }
}
