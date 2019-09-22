namespace NotificationApi
{
    public interface INotificationService
    {
        void NotifyAboutDeliveryStart(DeliveryStartNotification data);

        void NotifyAboutDeliveryFinish(DeliveryFinishNotification data);
    }
}
