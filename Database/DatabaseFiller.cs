using Extensions.Hosting.AsyncInitialization;
using Microsoft.Extensions.Logging;
using OptimaTrackerWebService.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OptimaTrackerWebService.Database
{
    public class DatabaseFiller : IAsyncInitializer
    {
        private readonly DatabaseContext dbContext;
        private readonly ILogger log;
        public DatabaseFiller(DatabaseContext databaseContext, ILogger<DatabaseFiller> logger)
        {
            dbContext = databaseContext;
            log = logger;
        }

        public async Task InitializeAsync()
        {
            var fillProceduresTable = await FillProceduresDictIfEmpty();
            log.LogInformation(fillProceduresTable);
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
                return Task.FromResult("ProceduresDict filled!");
            }
            return Task.FromResult("ProceduresDict is not empty!");
        }

        private static string[] FillProceduresDict()
        {
            XDocument procedureXml = XDocument.Load("procedures.xml");
            string[] procedures = procedureXml.Root.Descendants("Procedure").Select(e => e.Attribute("Name").Value).ToArray();

            return procedures;
        }
    }
}
