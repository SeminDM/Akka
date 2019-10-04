using System;
using System.Collections.Generic;
using System.Text;

namespace Api
{
    public interface ITransportService
    {
        TransportData GetTransportInfo(GoodsData goodsData);
    }
}
