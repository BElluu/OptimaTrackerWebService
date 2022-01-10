using System.Collections.Generic;

namespace OptimaTrackerWebService.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string SerialKey { get; set; }
        public string TIN { get; set; }

        public virtual List<Event> Events { get; set; }

       // public Event Event { get; set; }
    }
}
