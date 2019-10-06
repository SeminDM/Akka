using NotificationApi;
using System.IO;

namespace NotificationCore
{
    public class NotificationService : INotificationService
    {
        public void NotifyAboutDeliveryStart(DeliveryStartNotification data)
        {
            var path = @"E:\notify.txt";
            var msg = $"{data.Good} will be delivered";
            File.AppendAllLines(path, new[] { msg });
        }

        public void NotifyAboutDeliveryFinish(DeliveryFinishNotification data)
        {
            var path = @"E:\notify.txt";
            var msg = data.IsSuccess 
                ? $"{data.Good} are delivered by {data.TransportType} {data.ShipId} successfully"
                : $"Delivery of {data.Good} by {data.TransportType} {data.ShipId} was failed";
            File.AppendAllLines(path, new[] { msg });
        }
    }
}
