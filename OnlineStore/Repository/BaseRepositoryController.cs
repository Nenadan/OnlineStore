using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using OnlineStore.Models.BaseModels;
using System.Net;

namespace OnlineStore.Repository
{
    public class BaseRepositoryController<Titem> : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private CosmosClient _cosmosClient;
        private string _docType;
        private string _containerName;
        private Container _container;

        public BaseRepositoryController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("COSMOS_DB") ?? "N/A";

            _cosmosClient = new CosmosClient(_connectionString);

            Type type = typeof(Titem);
            CosmosAttribute attribute = (CosmosAttribute) Attribute.GetCustomAttribute(type, typeof(CosmosAttribute));
            _docType = attribute.DocType;
            _containerName = attribute.Container;

            _container = _cosmosClient.GetContainer(databaseId: "OnlineStore", containerId: _containerName); 

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseModel))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiResponseModel))]
        public async Task<ApiResponseModel> Create([FromBody]Titem model)
        {
            var response = await _container.CreateItemAsync<Titem>(item: model, partitionKey: new PartitionKey(_docType));
            var result = new ApiResponseModel()
            {
                StatuseCode = response.StatusCode,
                ResponseContent = model,
                IsSuccessfull = response.StatusCode == HttpStatusCode.Created,
            };
            return result;
        }
    }
}
