using Confluent.Kafka;
using KafkaPoc.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace KafkaPoc.Infrastructure.Services;

public class KafkaConsumerService : IKafkaConsumerService, IDisposable
{
    private readonly ILogger<KafkaConsumerService> _logger;
    private readonly IConsumer<string, string> _consumer;
    private const string _topic = "poems";
    private bool _disposed;

    public KafkaConsumerService(
        ILogger<KafkaConsumerService> logger,
        IConsumer<string, string> consumer)
    {
        _logger = logger;
        _consumer = consumer;
        _consumer.Subscribe(_topic);
        _logger.LogInformation("Kafka consumer initialized for topic: {Topic}", _topic);
    }

    public async Task<string?> ConsumeAsync(CancellationToken cancellationToken)
    {
        try
        {
            var consumeResult = _consumer.Consume(cancellationToken);
            return consumeResult?.Message?.Value;
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Consumption cancelled");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error consuming message from Kafka");
            throw;
        }
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _consumer?.Close();
            _consumer?.Dispose();
            _disposed = true;
        }

        GC.SuppressFinalize(this);
    }
}