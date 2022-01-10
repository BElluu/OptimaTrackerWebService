using OptimaTrackerWebService.Database;
using OptimaTrackerWebService.Models;
using System;
using System.Linq;

namespace OptimaTrackerWebService.Services
{
    public class DatabaseService : IDatabaseService
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
            try
            {
                if (!SerialKeyExists(data.SerialKey))
                    InsertCompanyData(data);

                int companyId = GetCompanyId(data.SerialKey);
                InsertEventsData(data, companyId);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //TODO Save JSON file here
            }
        }

        private void InsertCompanyData(Company data)
        {
            var companyData = new Company
            {
                SerialKey = data.SerialKey,
                TIN = data.TIN
            };
            dbContext.companies.Add(companyData);
            dbContext.SaveChanges();
        }

        private void InsertEventsData(Company data, int companyId)
        {
            foreach (var abc in data.Events)
            {
                var eventDefinitionId = GetEventDefinitionId(abc.ProcedureId);
                if (eventDefinitionId != 0)
                {
                    var eventData = new Event
                    {
                        ProcedureIdentity = eventDefinitionId,
                        NumberOfOccurrences = abc.NumberOfOccurrences,
                        CompanyId = companyId,
                        TimeStamp = DateTime.Today

                    };
                    dbContext.events.Add(eventData);
                    dbContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine(abc.ProcedureId + " do not exists in events dictionary");
                }
            }
        }

        private bool SerialKeyExists(string serialKey)
        {
            string GetSerialKey = dbContext.companies.Where(c => c.SerialKey == serialKey).Select(c => c.SerialKey).SingleOrDefault();

            if (GetSerialKey == serialKey)
                return true;

            return false;
        }
        private int GetCompanyId(string serialKey)
        {
            int companyId = dbContext
                .companies.Where(c => c.SerialKey == serialKey)
                .Select(c => c.Id)
                .SingleOrDefault();

            return companyId;
        }

        private int GetEventDefinitionId(string procedureId)
        {
            int eventDefinitionId = dbContext.eventsDict.Where(d => d.ProcedureId == procedureId).Select(d => d.Id).SingleOrDefault();

            return eventDefinitionId;
        }
    }
}

