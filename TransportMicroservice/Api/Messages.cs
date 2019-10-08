using System;

namespace Api
{
    public class GoodsData
    {
        public int Length { get; }
        public int Height { get; }
        public int Width  { get; }
        public int Weight { get; }

        public GoodsData(int Length, int Height, int Width, int Weight)
        {
            this.Length = Length;
            this.Height = Height;
            this.Width  = Width;
            this.Weight = Weight;
        }
    }

    public class TransportData
    {
        public int VehicleNumber           { get; }
        public DateTime DepartureDate      { get; }
        public TransportType TransportType { get; }

        public TransportData(int VehicleNumber, DateTime DepartureDate, TransportType TransportType)
        {
            this.VehicleNumber = VehicleNumber;
            this.DepartureDate = DepartureDate;
            this.TransportType = TransportType;
        }
    }

    public enum TransportType
    {
        Undefined,
        Ship,
        Plane,
        Train
    }
}
