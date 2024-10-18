using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using FinancialService.Model;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FinancialService.Functions
{
    public class CartChangeFeedFunction
    {
        
        private readonly IConfiguration _configuration;
        private readonly string _topicName;
        private readonly string _namespaceName;
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ServiceBusSender _sender;

        public CartChangeFeedFunction(IConfiguration configuration)
        {
            _configuration = configuration;
            _namespaceName = _configuration.GetValue<string>("NameSpace");
            _topicName = _configuration.GetValue<string>("TopicName");
            _serviceBusClient = new ServiceBusClient(_namespaceName);
            _sender = _serviceBusClient.CreateSender(_topicName);
        }
        
        [FunctionName("CartUpdatesChangeFeedProcessor")]
        public async Task Run([CosmosDBTrigger(
            databaseName: "OnlineStore",
            containerName: "Financial",
            Connection = "COSMOS_DB",
            LeaseContainerName = "leases",
            CreateLeaseContainerIfNotExists = true)]IReadOnlyList<ToDoItem> input,
            ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].id);

                CartUpdate cartUpdates = new()
                {
                    id = Guid.NewGuid(),
                    OriginalId = input[0].id,
                    Description = input[0].Description,
                };

                var messageBody = JsonSerializer.Serialize(cartUpdates);

                ServiceBusMessage message = new ServiceBusMessage(messageBody);
                await _sender.SendMessageAsync(message);
            }
        }
    }

    // Customize the model with your own desired properties
    public class ToDoItem
    {
        public string id { get; set; }
        public string Description { get; set; }
    }

    public class CartUpdate
    {
        public Guid id { get; set; }
        public string OriginalId { get; set; }
        public string Description { get; set; }
    }
}
