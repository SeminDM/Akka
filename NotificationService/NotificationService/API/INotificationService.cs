namespace NotificationService
{
    public interface INotificationService
    {
        void NotifyAboutDeliveryStart(DelivaryStartNotification data);

        void NotifyAboutDeliveryFinish(DelivaryFinishNotification data);
    }
}
