using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using KafkaPoc.Domain.Entities;
using KafkaPoc.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;

namespace KafkaPoc.Infrastructure.Services;

public class KafkaMessageProducer : IMessageProducer
{
    private readonly IProducer<string, TestObject> _producer;

    public KafkaMessageProducer(IOptions<ProducerConfig> config)
    {
        var schemaRegistryConfig = new SchemaRegistryConfig
        {
            Url = "localhost:8081"
        };
        var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);

        var jsonSerializerConfig = new JsonSerializerConfig
        {
            BufferBytes = 100
        };

        var producerConfig = config.Value;
        _producer = new ProducerBuilder<string, TestObject>(producerConfig)
            .SetValueSerializer(new JsonSerializer<TestObject>(schemaRegistry, jsonSerializerConfig))
            .Build();
    }

    public async Task ProduceAsync(TestObject message, CancellationToken cancellationToken = default)
    {
        var kafkaMessage = new Message<string, TestObject>
        {
            Key = Guid.NewGuid().ToString(),
            Value = message
        };

        await _producer.ProduceAsync("kubera.statement.line.importer", kafkaMessage, cancellationToken);
    }
}
