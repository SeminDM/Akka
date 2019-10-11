using System.Threading.Tasks;

namespace DeliveryApi
{
    public interface IDeliveryService
    {
        DeliveryResult DeliverGoods(DeliveryGoods data, Api.TransportData transportInfo);
        Task<DeliveryResult> DeliverGoodsAsync(DeliveryGoods data, Api.TransportData transportInfo);
    }
}
