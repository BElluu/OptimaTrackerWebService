using Extensions.Hosting.AsyncInitialization;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace OptimaTrackerWebService.Database
{
    public class DatabaseCreator : IAsyncInitializer
    {
        private readonly DatabaseContext dbContext;
        private readonly ILogger log;


        public DatabaseCreator(DatabaseContext databaseContext, ILogger<DatabaseCreator> logger)
        {
            dbContext = databaseContext;
            log = logger;
        }
        public async Task InitializeAsync()
        {
            var dbCreating = await CreateDatabaseIfNotExist();
            log.LogInformation(dbCreating);
        }

        private async Task<string> CreateDatabaseIfNotExist()
        {
            if (await dbContext.Database.EnsureCreatedAsync())
            {
                return await Task.FromResult("The database has been created.");
            }
            return await Task.FromResult("Database already exists.");
        }
    }
}
