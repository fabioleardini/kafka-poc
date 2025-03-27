using System.Text.Json;
using System.Text.Json.Nodes;
using KafkaPoc.Domain.Entities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace KafkaPoc.Functions.Functions
{
    public class KafkaMessageFunction(ILogger<KafkaMessageFunction> logger)
    {
        private readonly ILogger<KafkaMessageFunction> _logger = logger;

        [Function("KafkaTrigger")]
        public void Run(
            [KafkaTrigger("pkc-56d1g.eastus.azure.confluent.cloud:9092",
                        "poems",
                        Username = "33HPXS7RNWSWB7RX",
                        Password = "b9Y8ohtidqNrr1BQOrXhv6lhu0oghYhLepWhQKwxu4HQOUX8wt2NZozgSkuQa0m6",
                        Protocol = BrokerProtocol.SaslSsl,
                        AuthenticationMode = BrokerAuthenticationMode.Plain,
                        ConsumerGroup = "$Default")]
            string eventData)
        {
            try
            {
                JsonNode? jsonNode = JsonNode.Parse(eventData);
                string? message = jsonNode["Value"]?.GetValue<string>();

                _logger.LogInformation($"Message received: {message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing message: {ex.Message}");
            }
        }

        //[Function("KafkaTriggerMany")]
        public void RunMany(
            [KafkaTrigger("pkc-56d1g.eastus.azure.confluent.cloud:9092",
                        "poems",
                        Username = "33HPXS7RNWSWB7RX",
                        Password = "b9Y8ohtidqNrr1BQOrXhv6lhu0oghYhLepWhQKwxu4HQOUX8wt2NZozgSkuQa0m6",
                        Protocol = BrokerProtocol.SaslSsl,
                        AuthenticationMode = BrokerAuthenticationMode.Plain,
                        ConsumerGroup = "$Default")]
            string[] events)
        {
            foreach (string eventData in events)
            {
                try
                {
                    var message = JsonSerializer.Deserialize<Message>(eventData);
                    _logger.LogInformation($"Message received: {message?.Id} - {message?.Content}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error processing message: {ex.Message}");
                }
            }
        }
    }
}