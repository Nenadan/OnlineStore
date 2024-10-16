using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;
using OnlineStore.Repository;

namespace OnlineStore.Controllers.CartController;

[Route("api/cart")]
[ApiController]
public class CartController : BaseRepositoryController<CartModel>
{
    public CartController(IConfiguration configuration) : base(configuration) { }

}
