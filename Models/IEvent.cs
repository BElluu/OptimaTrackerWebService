using System;

namespace OptimaTrackerWebService.Models
{
    public interface IEvent
    {
        public int Id { get; set; }
        public int ProcedureId { get; set; }
        public int NumberOfOccurrences { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
