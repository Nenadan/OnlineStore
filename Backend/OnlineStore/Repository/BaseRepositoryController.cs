using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using OnlineStore.Models.BaseModels;
using OnlineStore.Repository.Base;
using System.Net;

namespace OnlineStore.Repository
{
    public class BaseRepositoryController<Titem> : CosmosDbConnectionBase<Titem> where Titem : BaseApiModel<Guid>
    {
        public BaseRepositoryController(IConfiguration configuration) : base(configuration) { }

        [HttpGet("{id}")]
        public async Task<Titem> GetById(Guid id)
        {
            IOrderedQueryable<Titem> queryable = Container.GetItemLinqQueryable<Titem>();
            var result = queryable.Where(x => x.Id == id).ToFeedIterator();
            var response = await result.ReadNextAsync();
            return response.First();
        }

        [HttpGet("getall")]
        public async Task<List<Titem>> GetAll()
        {
            var query = Container.GetItemLinqQueryable<Titem>().ToFeedIterator();
            
            List<Titem> results = new();

            while(query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.Resource);
            }
            return results;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponseModel))]
        public async Task<ApiResponseModel> Create([FromBody]Titem model)
        {
            var response = await Container.CreateItemAsync<Titem>(item: model, partitionKey: new PartitionKey(DocType));
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
