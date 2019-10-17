using System;

namespace Api
{
    public class GoodsData
    {
        public int Length { get; }
        public int Height { get; }
        public int Width { get; }
        public int Weight { get; }
        public string Description { get; set; }
        public GoodsData(int length, int height, int width, int weight, string description)
        {
            Length = length;
            Height = height;
            Width = width;
            Weight = weight;
            Description = description;
        }
    }

    public class TransportData
    {
        public int VehicleNumber { get; }
        public DateTime DepartureDate { get; }
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