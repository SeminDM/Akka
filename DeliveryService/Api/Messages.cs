using System;

namespace DeliveryApi
{
    public class DeliveryResult
    {
        public TransportType TransportType { get; set; }

        public string ShipId { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string Address { get; set; }

        public bool IsSuccess { get; set; }
    }

    public class DeliveryGoods
    {
        public string[] Goods { get; set; }
    }

    public enum TransportType
    {
        Undefined,
        Ship,
        Plain,
        Train
    }
}
