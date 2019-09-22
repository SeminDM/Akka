using System;

namespace NotificationApi
{
    public class DeliveryStartNotification
    {
        public string ShipId { get; set; }

        public TransportType TransportType { get; set; }
    }

    public class DeliveryFinishNotification
    {
        public string ShipId { get; set; }

        public DateTime DeliveryDate { get; set; }

        public bool IsSuccess { get; set; }
    }

    public enum TransportType
    {
        Undefined,
        Ship,
        Plane,
        Train
    }
}
