using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;
using OnlineStore.Models.BaseModels;
using OnlineStore.Repository.Product;

namespace OnlineStore.Controllers.ProductController
{
    [ApiController]
    [Route("api/v2/product")]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        [Route("/create")]
        public async Task<ApiResponseModel> CreateProduct([FromBody]ProductModel product)
        {
            var response = await _productRepository.Create(product);
            return response;
        }
    }
}
