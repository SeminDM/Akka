using NotificationApi;
using System.IO;

namespace NotificationCore
{
    public class NotificationService : INotificationService
    {
        public void NotifyAboutDeliveryStart(DeliveryStartNotification data)
        {
            var path = @"C:\Users\mosip\Documents\temp\notify.txt";
            var msg = $"Goods will be delivered by {data.TransportType} {data.ShipId}";
            File.WriteAllText(path, msg);
        }

        public void NotifyAboutDeliveryFinish(DeliveryFinishNotification data)
        {
            var path = @"C:\Users\mosip\Documents\temp\notify.txt";
            var msg = data.IsSuccess 
                ? $"Your goods are delivered by {data.ShipId} successfully"
                : $"Delivery of your goods was failed";
            File.WriteAllText(path, msg);
        }
    }
}
