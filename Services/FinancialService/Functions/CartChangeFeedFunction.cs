using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using FinancialService.Model;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FinancialService.Functions
{
    public sealed class CartChangeFeedFunction
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public CartChangeFeedFunction(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("COSMOS_DB");
        }

        [FunctionName(nameof(ChangeFeedProcessorForFinancialContainer))]
        public static void ChangeFeedProcessorForFinancialContainer([CosmosDBTrigger(
            databaseName: "OnlineStore",
            containerName: "Financial",
            Connection = "COSMOS_DB",
            LeaseContainerName = "leases",
            CreateLeaseContainerIfNotExists = true)]IReadOnlyList<ToDoItem> input,
            ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                var json = JsonSerializer.Serialize(input[0]);
                var response = JsonSerializer.Deserialize<CartModel>(json);
                if(response != null)
                {
                    log.LogInformation($"Item has been updated with Id: {response.Id} and Amount: {response.Amount}.");
                }
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].id);
            }
        }
    }

    // Customize the model with your own desired properties
    public class ToDoItem
    {
        public string id { get; set; }
        public string Description { get; set; }
    }
}
