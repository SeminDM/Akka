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
                    var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("config.json");
                    _config = builder.Build();
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
