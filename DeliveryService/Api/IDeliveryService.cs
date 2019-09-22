namespace DeliveryApi
{
    public interface IDeliveryService
    {
        DeliveryResult DeliverGoods(DeliveryData data);
    }
}
