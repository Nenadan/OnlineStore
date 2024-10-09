using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using OnlineStore.Models.BaseModels;
using OnlineStore.Repository.Base;
using OnlineStore.Repository.BaseRepository;
using System.Net;

namespace OnlineStore.Repository.BaseRepository
{
    public class BaseRepositoryMethods<TItem> : CosmosDbConnectionBase<TItem> where TItem : BaseApiModel<Guid>
    {
        public BaseRepositoryMethods(IConfiguration configuration) : base(configuration)
        {
            
        }

        public async Task<ApiResponseModel> Create([FromBody] TItem model)
        {
            var response = await Container.CreateItemAsync<TItem>(item: model, partitionKey: new PartitionKey(DocType));
            var result = new ApiResponseModel()
            {
                StatuseCode = response.StatusCode,
                ResponseContent = model,
                IsSuccessfull = response.StatusCode == HttpStatusCode.Created,
            };
            return result;
        }

        public async Task<TItem> GetById(Guid id)
        {
            IOrderedQueryable<TItem> queryable = Container.GetItemLinqQueryable<TItem>();
            var result = queryable.Where(x => x.Id == id).ToFeedIterator();
            var response = await result.ReadNextAsync();
            return response.First();
        }

        public async Task<List<TItem>> GetAll()
        {
            var query = Container.GetItemLinqQueryable<TItem>().ToFeedIterator();

            List<TItem> results = new();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.Resource);
            }
            return results;
        }
    }
}
