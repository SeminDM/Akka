using Akka.Actor;

namespace NotificationService
{
    public class NotificationActor : ReceiveActor
    {
        private readonly INotificationService _service;

        public NotificationActor()
        {
            _service = new NotificationService();

            Receive<DelivaryStartNotification>(msg =>
            {
                _service.NotifyAboutDeliveryStart(msg);
            });

            Receive<DelivaryFinishNotification>(msg =>
            {
                _service.NotifyAboutDeliveryFinish(msg);
            });
        }
    }
}
