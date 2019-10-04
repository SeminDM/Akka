using System;
using System.Collections.Generic;
using System.Text;

namespace Api
{
    public class GoodsData
    {
        public int Length { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Weight { get; set; }
        public TransportType TranspType { get; set; }
    }

    public class TransportData
    {
        public int VehicleNumber { get; set; }
        public DateTime DepartureDate { get; set; }
    }

    public enum TransportType
    {
        Undefined,
        Ship,
        Plain,
        Train
    }
}
