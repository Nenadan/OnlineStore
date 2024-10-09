using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;
using OnlineStore.Models.BaseModels;
using OnlineStore.Repository.Cart;

namespace OnlineStore.Controllers.CartController
{
    [Route("api/v2/cart")]
    [ApiController]
    public class CartV2Controller
    {
        public readonly ICartRepository _cartRepository;
        public CartV2Controller(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpPost]
        public async Task<ApiResponseModel> CreateCart([FromBody] CartModel cart)
        {
            var response = await _cartRepository.Create(cart);
            return response;
        }
    }
}
