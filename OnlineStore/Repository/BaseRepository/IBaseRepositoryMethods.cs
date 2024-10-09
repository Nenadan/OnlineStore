using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models.BaseModels;

namespace OnlineStore.Repository.BaseRepository
{
    public interface IBaseRepositoryMethods<TItem>
    {
        public Task<TItem> GetById(Guid id);
        public Task<List<TItem>> GetAll();
        public Task<ApiResponseModel> Create([FromBody] TItem model);
    }
}
