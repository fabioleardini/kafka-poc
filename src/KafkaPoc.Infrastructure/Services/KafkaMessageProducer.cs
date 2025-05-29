using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using KafkaPoc.Domain.Entities;
using KafkaPoc.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using System.Text;

namespace KafkaPoc.Infrastructure.Services;

public class KafkaMessageProducer : IMessageProducer
{
    private readonly IProducer<string, StatementLineImporterMessage> _producer;

    public KafkaMessageProducer(IOptions<ProducerConfig> config)
    {
        var schemaRegistryConfig = new SchemaRegistryConfig
        {
            Url = "localhost:8081",
            BasicAuthCredentialsSource = AuthCredentialsSource.UserInfo
        };
        var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);

        var producerConfig = config.Value;
        _producer = new ProducerBuilder<string, StatementLineImporterMessage>(producerConfig)
            .SetValueSerializer(new JsonSerializer<StatementLineImporterMessage>(schemaRegistry))
            .Build();
    }

    public async Task ProduceAsync(StatementLineImporterMessage message, CancellationToken cancellationToken = default)
    {
        var kafkaMessage = new Message<string, StatementLineImporterMessage>
        {
            Key = Guid.NewGuid().ToString(),
            Headers = new Headers
            {
                { "X-Correlation-ID", Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())}
            },
            Value = message
        };

        await _producer.ProduceAsync("kubera.statement.line.importer", kafkaMessage, cancellationToken);
    }
}
