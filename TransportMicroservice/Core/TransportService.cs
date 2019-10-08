using Api;
using System;
using System.IO;

namespace Core
{
    public class TransportService : ITransportService
    {
        Random rnd = new Random();

        public TransportData GetTransportInfo(GoodsData goodsData)
        {
            var numb = rnd.Next(1, 3);
            var path = Configurator.GetValue<string>("PathToSaveTranspotrResult");

            var id = rnd.Next(10000, 50000);
            var type = (TransportType)numb;

            var msg = $"Width: {goodsData.Width} Length: {goodsData.Length} => Transport: {id} - {type}";
            
            WriteInFile(msg, path);
            return new TransportData(id, RandomDate(), type);
        }

        public DateTime RandomDate()
        {
            DateTime startdate = new DateTime(2014, 1, 1, 7, 0, 0);
            return startdate.AddMinutes(rnd.Next(Int32.MaxValue)).ToUniversalTime();
        }

        private static void WriteInFile(string message, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLineAsync(message);
            }
        }
    }
}
