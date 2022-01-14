using System;
using Newtonsoft.Json;

namespace OptimaTrackerWebService.Models
{
    public class Event
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string ProcedureName { get; set; }
        [JsonIgnore]
        public int ProcedureId { get; set; }
        public int NumberOfOccurrences { get; set; }
        [JsonIgnore]
        public int CompanyId { get; set; }
        [JsonIgnore]
        public DateTime TimeStamp { get; set; }
        [JsonIgnore]
        public Company Company { get; set; }

    }
}
