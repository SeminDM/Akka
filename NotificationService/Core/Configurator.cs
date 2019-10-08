using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NotificationCore
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
                    string currentDirectory = Directory.GetCurrentDirectory();
                    conBuilder.AddJsonFile(currentDirectory + "/appsettings.json");
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
