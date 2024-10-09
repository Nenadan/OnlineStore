using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using OnlineStore.Models.BaseModels;

namespace OnlineStore.Repository
{
    public class BaseRepositoryController<Titem> : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private CosmosClient _cosmosClient;

        public BaseRepositoryController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("COSMOS_DB") ?? "N/A";

            _cosmosClient = new CosmosClient(_connectionString);
        }

        [HttpGet]
        public ApiResponseModel Index()
        {
            
        }
    }
}
