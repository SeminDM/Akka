using System;

namespace DeliveryService
{
    public class DeliveryResult
    {
        public DateTime DeliveryDate { get; set; }

        public string Address { get; set; }

        public bool IsSuccess { get; set; }
    }

    public class DeliveryData
    {
        public string[] Goods { get; set; }

        public string ShipId { get; set; }

        public TransportType TransportType { get; set; }
    }

    public enum TransportType
    {
        Undefined,
        Ship,
        Plain,
        Train
    }
}
