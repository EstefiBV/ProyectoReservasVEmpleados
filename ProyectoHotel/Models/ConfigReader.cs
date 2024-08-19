using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;

namespace ProyectoHotel.Models
{
    public static class ConfigReader
    {
        public static AmadeusAPIConfig ReadAmadeusAPIConfig()
        {
            var configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            var json = File.ReadAllText(configFilePath);
            dynamic config = JsonConvert.DeserializeObject(json);
            var apiConfig = new AmadeusAPIConfig
            {
                ApiKey = config.AmadeusAPI.ApiKey,
                ApiSecret = config.AmadeusAPI.ApiSecret
            };
            return apiConfig;
        }
    }
}