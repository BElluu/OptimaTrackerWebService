using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimaTrackerWebService.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string ProcedureId { get; set; }
        public int NumberOfOccurrences { get; set; }
        public int CompanyId { get; set; }
        public DateTime TimeStamp { get; set; }

        public Company Company { get; set; }

    }
}
