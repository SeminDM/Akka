using Akka.Actor;
using NotificationApi;
using NotificationCore;

namespace NotificationActors
{
    public class NotificationActor : ReceiveActor
    {
        private readonly INotificationService _service;

        public NotificationActor()
        {
            _service = new NotificationService();

            Receive<DeliveryStartNotification>(msg =>
            {
                _service.NotifyAboutDeliveryStart(msg);
            });

            Receive<DeliveryFinishNotification>(msg =>
            {
                _service.NotifyAboutDeliveryFinish(msg);
            });
        }
    }
}
