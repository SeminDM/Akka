using NotificationApi;
using System.IO;

namespace NotificationCore
{
    public class NotificationService : INotificationService
    {
        public void NotifyAboutDeliveryStart(DeliveryStartNotification data)
        {
            var path = @"notify.txt";
            var msg = $"Goods will be delivered by {data.TransportType} {data.ShipId}";
            File.AppendAllLines(path, new[] { msg });
        }

        public void NotifyAboutDeliveryFinish(DeliveryFinishNotification data)
        {
            var path = @"notify.txt";
            var msg = data.IsSuccess 
                ? $"Your goods are delivered by {data.ShipId} successfully"
                : $"Delivery of your goods was failed";
            File.AppendAllLines(path, new[] { msg });
        }
    }
}
