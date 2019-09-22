using Akka.Actor;

namespace AkkaShop
{
    public delegate IActorRef DeliveryActorProvider();

    public delegate IActorRef NotificationActorProvider();
}
