using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core
{
    public static class Configurator
    {
        private static IConfiguration _config { get; set; }
        public static IConfiguration config
        {
            get
            {
                if (_config == null)
                {
                    var conBuilder = new ConfigurationBuilder();
                    string currentDirectory = @"C:\Users\mosip\Documents\Microservices-with-Akka.NetDM\TransportMicroservice\Core";
                    conBuilder.AddJsonFile(currentDirectory + "/config.json");
                    _config = conBuilder.Build();
                }
                return _config;
            }
            private set
            {
                _config = value;
            }
        }
        public static T GetValue<T>(string key)
        {
            return config.GetValue<T>(key);
        }
    }
}
