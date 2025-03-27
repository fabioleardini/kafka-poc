using KafkaPoc.Infrastructure.Interfaces;

namespace KafkaPoc.Worker.Services;

public class KafkaConsumerWorker(
    ILogger<KafkaConsumerWorker> logger,
    IKafkaConsumerService kafkaConsumerService) : BackgroundService
{
    private readonly ILogger<KafkaConsumerWorker> _logger = logger;
    private readonly IKafkaConsumerService _kafkaConsumerService = kafkaConsumerService;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var message = await _kafkaConsumerService.ConsumeAsync(stoppingToken);
                if (message != null)
                {
                    _logger.LogInformation("Received message: {Message}", message);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while consuming messages");
            throw;
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Stopping Kafka consumer worker");
        await base.StopAsync(stoppingToken);
    }
}