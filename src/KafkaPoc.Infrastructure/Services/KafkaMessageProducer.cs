using Confluent.Kafka;
using KafkaPoc.Domain.Entities;
using KafkaPoc.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace KafkaPoc.Infrastructure.Services
{
    public class KafkaMessageProducer(IOptions<ProducerConfig> config) : IMessageProducer
    {
        private readonly IProducer<string, string> _producer = new ProducerBuilder<string, string>(config.Value).Build();

        public async Task ProduceAsync(Message message, CancellationToken cancellationToken = default)
        {
            var kafkaMessage = new Message<string, string>
            {
                Key = message.Id,
                Value = JsonSerializer.Serialize(message)
            };

            await _producer.ProduceAsync(message.Topic, kafkaMessage, cancellationToken);
        }
    }
}