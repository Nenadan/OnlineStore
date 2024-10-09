using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using OnlineStore.Models;
using OnlineStore.Models.BaseModel;

namespace OnlineStore.Controllers.CartController;

[ApiController]
[Route("cart")]
public class CartController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public CartController(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    [HttpGet]
    public async Task<string> GetCarts()
    {
        var connectionString = _configuration.GetConnectionString("COSMOS_DB");
        CosmosClient client = new CosmosClient(connectionString);

        await client.CreateDatabaseIfNotExistsAsync("OnlineStore");
        var database = client.GetDatabase("OnlineStore");

        Container financial = await database.CreateContainerIfNotExistsAsync("Financial", "/DocType");

        Type type = typeof(CartModel);
        var attribute = (CosmosAttribute) Attribute.GetCustomAttribute(type, typeof(CosmosAttribute));
       
        

        //financial.CreateItemAsync

        return attribute.DocType;
    }
}
