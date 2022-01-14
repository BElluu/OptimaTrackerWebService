using Microsoft.Extensions.Configuration;
using OptimaTrackerWebService.Models;
using System;
using System.Linq;
using System.Xml.Linq;

namespace OptimaTrackerWebService.Database
{
    public class DatabaseFiller
    {
        private readonly IConfiguration configuration;
        public DatabaseFiller(IConfiguration config)
        {
            configuration = config;
        }

        public void FillProceduresDictIfEmpty()
        {
            using (var dbContext = new DatabaseContext(configuration))
            {
                if (!dbContext.proceduresDict.Any())
                {
                    var procedures = FillProceduresDict();
                    int x = 1;
                    foreach (var procedure in procedures)
                    {
                        var eDict = new ProceduresDict { Id = x, ProcedureName = procedure };
                        dbContext.proceduresDict.Add(eDict);
                        x++;

                    }
                    dbContext.SaveChanges();
                }
            }
            Console.WriteLine("ProceduresDict filled!");
        }
        private string[] FillProceduresDict()
        {
            XDocument procedureXml = XDocument.Load(configuration["OtherSettings:ProceduresFileLocation"]);
            string[] procedures = procedureXml.Root.Descendants("Procedure").Select(e => e.Attribute("Name").Value).ToArray();

            return procedures;
        }
    }
}
