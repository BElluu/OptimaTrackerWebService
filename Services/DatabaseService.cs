using OptimaTrackerWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimaTrackerWebService.Services
{
    public class DatabaseService
    {
        public void Insert(EventModel Event)
        {
            Console.WriteLine(Event.SerialKey);
            Console.WriteLine(Event.TIN);
            Console.WriteLine(Event.Events);
        }
    }
}
