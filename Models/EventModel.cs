using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimaTrackerWebService.Models
{
    public class EventModel
    {
        public string SerialKey { get; set; }
        public string TIN { get; set; }
        public Event []Events { get; set; }
    }

    public class Event
    {
        public string ProcedureId { get; set; }
        public int NumberOfOccurrences { get; set; }
    }
}
