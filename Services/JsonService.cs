using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OptimaTrackerWebService.Models;
using System;
using System.IO;

namespace OptimaTrackerWebService.Services
{
    public class JsonService : IJsonService
    {
        private readonly IConfiguration configuration;

        public JsonService(IConfiguration config)
        {
            configuration = config;
        }
        public void CreateJsonFromObject(Company jsonObject)
        {
            string timestamp = (DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds).ToString();
            using (StreamWriter file = File.CreateText(configuration["OtherSettings:JsonFilePath"]+"ERR_" + timestamp +".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, jsonObject);
            }
        }
    }
}
