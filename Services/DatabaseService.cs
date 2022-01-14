using Microsoft.Extensions.Configuration;
using OptimaTrackerWebService.Database;
using OptimaTrackerWebService.Models;
using System;
using System.Linq;
using System.Xml.Linq;

namespace OptimaTrackerWebService.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly DatabaseContext dbContext;
        private readonly IJsonService json;
        private readonly IConfiguration configuration;

        public DatabaseService(DatabaseContext optimaTrackerContext, IJsonService jsonService, IConfiguration config)
        {
            dbContext = optimaTrackerContext;
            json = jsonService;
            configuration = config;
        }

        public void Insert(Company data)
        {
            try
            {
                //throw new Exception("Test Exception");
                if (!SerialKeyExists(data.SerialKey))
                    InsertCompanyData(data);

                int companyId = GetCompanyId(data.SerialKey);
                InsertEventsData(data, companyId);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                json.CreateJsonFromObject(data);

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
                var eventDefinitionId = GetEventDefinitionId(abc.ProcedureName);
                if (eventDefinitionId != 0)
                {
                    var eventData = new Event
                    {
                        ProcedureId = eventDefinitionId,
                        NumberOfOccurrences = abc.NumberOfOccurrences,
                        CompanyId = companyId,
                        TimeStamp = DateTime.Today

                    };
                    dbContext.events.Add(eventData);
                    dbContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine(abc.ProcedureName + " do not exists in events dictionary");
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

        private int GetEventDefinitionId(string procedureName)
        {
            int eventDefinitionId = dbContext.proceduresDict.Where(d => d.ProcedureName == procedureName).Select(d => d.Id).SingleOrDefault();

            return eventDefinitionId;
        }
    }
}

