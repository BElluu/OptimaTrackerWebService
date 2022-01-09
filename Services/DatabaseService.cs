using OptimaTrackerWebService.Database;
using OptimaTrackerWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimaTrackerWebService.Services
{
    public class DatabaseService: IDatabaseService
    {
        private readonly DatabaseContext dbContext;

        public DatabaseService(DatabaseContext optimaTrackerContext)
        {
            dbContext = optimaTrackerContext;
        }
        public void Insert(Company data)
        {
            /*            Console.WriteLine(data.SerialKey);
                        Console.WriteLine(data.TIN);
                        foreach (var abc in data.Events)
                        {
                            Console.WriteLine(abc.ProcedureId);
                            Console.WriteLine(abc.NumberOfOccurrences);
                        }*/

            var companyData = new Company
            {
                SerialKey = data.SerialKey,
                TIN = data.TIN
            };
            dbContext.companies.Add(companyData);
            dbContext.SaveChanges();

            foreach(var abc in data.Events)
            {
                var eventData = new Event
                {
                    ProcedureId = abc.ProcedureId,
                    NumberOfOccurrences = abc.NumberOfOccurrences

                };
                dbContext.events.Add(eventData);
                dbContext.SaveChanges();
            }

/*            using (var context = new DatabaseContext())
            {
                context.companies.Add(new Company
                {
                    SerialKey = data.SerialKey,
                    TIN = data.TIN
                });
                context.SaveChanges();
            }

            using (var context = new DatabaseContext())
            {
                foreach (var abc in data.Events) {
                    context.events.Add(new Event
                    {
                        ProcedureId = abc.ProcedureId,
                        NumberOfOccurrences = abc.NumberOfOccurrences
                   });
                    context.SaveChanges();
                }
            }*/

/*            using (var context = new DatabaseContext())
            {
                foreach (var abc in data.Events)
                {
                    context.companies.Add(new Company
                    {
                        SerialKey = data.SerialKey,
                        TIN = data.TIN,
                        Events = new List<Event>()
                        {
                            new Event {ProcedureId = abc.ProcedureId, NumberOfOccurrences = abc.NumberOfOccurrences}
                        }
                    });*/
/*                    context.Track.Add(new Track
                    {
                        SerialKey = data.SerialKey,
                        TIN = data.TIN,
                        Events = new List<Event>()
                                    {
                                       new Event{ ProcedureId = abc.ProcedureId, NumberOfOccurrences = abc.NumberOfOccurrences}
                                    }
                    });
                    context.SaveChanges();*/
                }
            }
        }
/*    }
}*/
