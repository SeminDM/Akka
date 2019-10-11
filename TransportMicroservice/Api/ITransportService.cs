using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api
{
    public interface ITransportService
    {
        TransportData GetTransportInfo(GoodsData goodsData);
        Task<TransportData> GetTransportInfoAsync(GoodsData goodsData);
    }
}
