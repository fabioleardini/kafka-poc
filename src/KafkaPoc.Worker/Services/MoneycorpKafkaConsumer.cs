
using Moneycorp.Kafka.Libraries.Connector.Interfaces;

namespace KafkaPoc.Worker.Services
{
    public class MoneycorpKafkaConsumer(ILogger<MoneycorpKafkaConsumer> logger,
    IMessageConsumer messageConsumer) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var message = await messageConsumer.ConsumeAsync(stoppingToken);
                    if (message != null)
                    {
                        logger.LogInformation("Received message: {Message}", message);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while consuming messages");
                throw;
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Stopping Kafka consumer worker");
            await base.StopAsync(stoppingToken);
        }
    }
}
