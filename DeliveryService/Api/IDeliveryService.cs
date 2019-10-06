namespace DeliveryApi
{
    public interface IDeliveryService
    {
        DeliveryResult DeliverGoods(DeliveryGoods data, Api.TransportData transportInfo);
    }
}
