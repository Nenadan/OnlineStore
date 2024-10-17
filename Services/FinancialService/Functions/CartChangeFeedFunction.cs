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
    public sealed class CartChangeFeedFunction
    {
        private readonly IConfiguration _configuration;
        private readonly string _queueName;
        private readonly string _namespaceName;
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ServiceBusSender _sender;

        public CartChangeFeedFunction(IConfiguration configuration)
        {
            _configuration = configuration;
            _namespaceName = _configuration.GetValue<string>("NameSpace");
            _queueName = _configuration.GetValue<string>("QueueName");
            //_serviceBusClient = new ServiceBusClient(_namespaceName);
            //_sender = _serviceBusClient.CreateSender(_queueName);
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
