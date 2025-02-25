using System.Text.Json;
using KafkaPoc.Domain.Entities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace KafkaPoc.Functions.Functions
{
    public class KafkaMessageFunction(ILogger<KafkaMessageFunction> logger)
    {
        private readonly ILogger<KafkaMessageFunction> _logger = logger;

        [Function(nameof(KafkaMessageFunction))]
        public void Run(
            [KafkaTrigger("localhost:29092",
                        "message-topic",
                        Username = "",
                        Password = "",
                        Protocol = BrokerProtocol.Plaintext,
                        AuthenticationMode = BrokerAuthenticationMode.Plain)]
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