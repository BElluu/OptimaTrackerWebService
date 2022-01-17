using Extensions.Hosting.AsyncInitialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OptimaTrackerWebService.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OptimaTrackerWebService.Database
{
    public class DatabaseFiller : IAsyncInitializer
    {
        private readonly IConfiguration configuration;
        private readonly DatabaseContext dbContext;
        private readonly ILogger log;
        public DatabaseFiller(IConfiguration config, DatabaseContext databaseContext, ILogger<DatabaseFiller> logger)
        {
            configuration = config;
            dbContext = databaseContext;
            log = logger;
        }

        public async Task InitializeAsync()
        {
            await FillProceduresDictIfEmpty();
        }

        public Task<string> FillProceduresDictIfEmpty()
        {
            if (!dbContext.proceduresDict.Any())
            {
                var procedures = FillProceduresDict();
                int x = 1;
                foreach (var procedure in procedures)
                {
                    var eDict = new ProceduresDict { Id = x, ProcedureName = procedure, IsEnabled = true };
                    dbContext.proceduresDict.Add(eDict);
                    x++;

                }
                dbContext.SaveChanges();
                log.LogInformation("ProceduresDict filled!");
                return Task.FromResult("ProceduresDict filled!");
            }
            log.LogInformation("ProceduresDict is not empty!");
            return Task.FromResult("ProceduresDict is not empty!");
        }

        private string[] FillProceduresDict()
        {
            XDocument procedureXml = XDocument.Load(configuration["OtherSettings:ProceduresFileLocation"]);
            string[] procedures = procedureXml.Root.Descendants("Procedure").Select(e => e.Attribute("Name").Value).ToArray();

            return procedures;
        }
    }
}
