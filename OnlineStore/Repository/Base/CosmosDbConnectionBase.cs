using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using OnlineStore.Models.BaseModels;

namespace OnlineStore.Repository.Base
{
    public class CosmosDbConnectionBase<TItem> : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly string _connectionString;

        private readonly CosmosClient _cosmosClient;

        public readonly string DocType;

        private readonly string _containerName;

        public readonly Container Container;

        public CosmosDbConnectionBase(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("COSMOS_DB") ?? "N/A";

            _cosmosClient = new CosmosClient(_connectionString);

            _cosmosClient.CreateDatabaseIfNotExistsAsync("OnlineStore", 400);

            var database = _cosmosClient.GetDatabase("OnlineStore");
            database.CreateContainerIfNotExistsAsync("Financial", "/DocType");

            Type type = typeof(TItem);
            CosmosAttribute attribute = (CosmosAttribute)Attribute.GetCustomAttribute(type, typeof(CosmosAttribute));
            DocType = attribute.DocType;
            _containerName = attribute.Container;

            Container = _cosmosClient.GetContainer(databaseId: "OnlineStore", containerId: _containerName);

        }
    }
}
