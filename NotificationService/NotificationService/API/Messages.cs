using System;

namespace NotificationService
{
    public class DelivaryStartNotification
    {
        public string ShipId { get; set; }

        public TransportType TransportType { get; set; }
    }

    public class DelivaryFinishNotification
    {
        public string ShipId { get; set; }

        public DateTime DeliveryDate { get; set; }

        public bool IsSuccess { get; set; }
    }

    public enum TransportType
    {
        Undefined,
        Ship,
        Plain,
        Train
    }
}
