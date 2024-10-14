using OnlineStore.Models;
using OnlineStore.Repository.BaseRepository;

namespace OnlineStore.Repository.Product
{
    public class ProductRepository : BaseRepositoryMethods<ProductModel>, IProductRepository
    {
        public ProductRepository(IConfiguration configuration) : base(configuration) { }
    }
}
