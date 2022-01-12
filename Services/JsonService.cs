using Newtonsoft.Json;
using OptimaTrackerWebService.Models;
using System;
using System.IO;

namespace OptimaTrackerWebService.Services
{
    public class JsonService : IJsonService
    {
        public void CreateJsonFromObject(Company jsonObject)
        {
            string timestamp = (DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds).ToString();
            using (StreamWriter file = File.CreateText(@"Z:\OptimaTracker\ERR_" + timestamp +".json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, jsonObject);
            }
        }
    }
}
