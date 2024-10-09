using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using OnlineStore.Models.BaseModels;
using System.Net;

namespace OnlineStore.Repository
{
    public class BaseRepositoryController<Titem> : ControllerBase where Titem : BaseApiModel<Guid>
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

        [HttpGet("{id}")]
        public async Task<Titem> GetById(Guid id)
        {
            IOrderedQueryable<Titem> queryable = _container.GetItemLinqQueryable<Titem>();
            var result = queryable.Where(x => x.Id == id).ToFeedIterator();
            var response = await result.ReadNextAsync();
            return response.First();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseModel))]
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
