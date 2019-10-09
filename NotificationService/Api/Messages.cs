using System;

namespace NotificationApi
{
    public class DeliveryStartNotification
    {
        public string Good { get; }

        public DeliveryStartNotification(string Good)
        {
            this.Good = Good;
        }
    }

    public class DeliveryFinishNotification
    {
        public string Good                  { get; }
        public string ShipId                { get; }
        public TransportType TransportType  { get; }
        public DateTime DeliveryDate        { get; }
        public bool IsSuccess               { get; set; }

        public DeliveryFinishNotification(string Good, string ShipId, TransportType TransportType, DateTime DeliveryDate, bool IsSuccess = false)
        {
            this.Good           = Good;
            this.ShipId         = ShipId;
            this.TransportType  = TransportType;
            this.DeliveryDate   = DeliveryDate;
            this.IsSuccess      = IsSuccess;
        }
    }

    public enum TransportType
    {
        Undefined,
        Ship,
        Plane,
        Train
    }
}
