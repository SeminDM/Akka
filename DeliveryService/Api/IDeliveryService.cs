using System.Threading.Tasks;

namespace DeliveryApi
{
    public interface IDeliveryService
    {
        Task<DeliveryResult> DeliverGoods(DeliveryGoods data, Api.TransportData transportInfo);
    }
}
