using OnlineStore.Models;
using OnlineStore.Repository.BaseRepository;

namespace OnlineStore.Repository.Cart
{
    public class CartRepository : BaseRepositoryMethods<CartModel>, ICartRepository
    {
        public CartRepository(IConfiguration configuration) : base(configuration) { }
    }
}
