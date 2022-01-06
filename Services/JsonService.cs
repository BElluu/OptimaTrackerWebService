using Newtonsoft.Json;
using OptimaTrackerWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimaTrackerWebService.Services
{
    public class JsonService
    {
        public EventModel Deserialize(string jsonString)
        {
            var eventObject = JsonConvert.DeserializeObject<EventModel>(jsonString);

            return eventObject;
        }
    }
}
