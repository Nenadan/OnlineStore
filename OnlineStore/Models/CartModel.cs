using OnlineStore.Models.BaseModel;

namespace OnlineStore.Models
{
    [Cosmos("Cart")]
    public class CartModel : BaseApiModel<Guid>
    {
        public int Amount { get; set; }
    }
}
