using System.IO;

namespace NotificationService
{
    public class NotificationService : INotificationService
    {
        public void NotifyAboutDeliveryStart(DelivaryStartNotification data)
        {
            var path = @"D:\notify.txt";
            var msg = $"Goods will be delivered by {data.TransportType} {data.ShipId}";
            File.WriteAllText(path, msg);
        }

        public void NotifyAboutDeliveryFinish(DelivaryFinishNotification data)
        {
            var path = @"D:\notify.txt";
            var msg = data.IsSuccess 
                ? $"Your goods are delivered by {data.ShipId} successfully"
                : $"Delivery of your goods was failed";
            File.WriteAllText(path, msg);
        }
    }
}
