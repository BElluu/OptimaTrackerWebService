using Extensions.Hosting.AsyncInitialization;
using Microsoft.Extensions.Configuration;
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
        public DatabaseFiller(IConfiguration config, DatabaseContext databaseContext)
        {
            configuration = config;
            dbContext = databaseContext;
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
                Console.WriteLine("ProceduresDict filled!");
                return Task.FromResult("ProceduresDict filled!");
            }
            Console.WriteLine("ProceduresDict is not empty!");
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
