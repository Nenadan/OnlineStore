using OnlineStore.Models.BaseModels;

namespace OnlineStore.Models
{
    [CosmosAttribute("Cart", "Financial")]
    public class CartModel : BaseApiModel<Guid>
    {
        public int Amount { get; set; }
    }
}
