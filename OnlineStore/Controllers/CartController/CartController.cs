using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using OnlineStore.Models;
using OnlineStore.Models.BaseModels;

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

       await database.CreateContainerIfNotExistsAsync("Financial", "/DocType");

        Container container = database.GetContainer("Financial");

        Type type = typeof(CartModel);
        var attribute = (CosmosAttribute) Attribute.GetCustomAttribute(type, typeof(CosmosAttribute));

        if(attribute != null)
        {
            var item = new CartModel()
            {
                Amount = 10,
                DocType = attribute.DocType,
                Id = Guid.NewGuid(),
            };

            await container.CreateItemAsync<CartModel>(item, partitionKey: new PartitionKey(attribute.DocType));
        }

        return attribute.DocType ?? "N/A";
    }
}
