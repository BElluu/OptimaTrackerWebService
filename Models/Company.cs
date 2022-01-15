using Newtonsoft.Json;
using System.Collections.Generic;

namespace OptimaTrackerWebService.Models
{
    public class Company
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string SerialKey { get; set; }
        public string TIN { get; set; }

        public virtual List<EventDetails> Events { get; set; }
    }
}
