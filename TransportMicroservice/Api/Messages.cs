using System;

namespace Api
{
    public class GoodsData
    {
        public int Length { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Weight { get; set; }
    }

    public class TransportData
    {
        public int VehicleNumber { get; set; }
        public DateTime DepartureDate { get; set; }
        public TransportType TransportType { get; set; }
    }

    public enum TransportType
    {
        Undefined,
        Ship,
        Plane,
        Train
    }
}
