using Microsoft.Azure.Cosmos;

namespace OnlineStore.Repository.Base
{
    public class PopulateDatabase : IPopulateDatabase
    {
        private readonly IConfiguration _configuration;
        private readonly string _databaseName;
        private readonly string _connectionString;
        private readonly CosmosClient _cosmosClient;
        public PopulateDatabase(IConfiguration configuration) 
        { 
            _configuration = configuration;
            _databaseName = configuration.GetValue<string>("DatabaseName") ?? "N/A";
            _connectionString = configuration.GetConnectionString("COSMOS_DB") ?? "N/A";

            _cosmosClient = new CosmosClient(_connectionString);
        }

        public async Task Run()
        {
            await InitializeDatabase();
            await InitializeContainers();
        }

        public async Task InitializeDatabase()
        {
            await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName);
        }

        public async Task InitializeContainers()
        {
            var database = _cosmosClient.GetDatabase(_databaseName);
            await database.CreateContainerIfNotExistsAsync(ContainerNames.Financial, "/DocType");
        }
    }
}
