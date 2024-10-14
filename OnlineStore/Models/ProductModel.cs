using OnlineStore.Models.BaseModels;

namespace OnlineStore.Models
{
    [CosmosAttribute("Product", "Financial")]
    public class ProductModel : BaseApiModel<Guid>
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
