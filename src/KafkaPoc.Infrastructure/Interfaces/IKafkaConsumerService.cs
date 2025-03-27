using System.Threading;
using System.Threading.Tasks;

namespace KafkaPoc.Infrastructure.Interfaces;

public interface IKafkaConsumerService
{
    /// <summary>
    /// Consumes a message from the Kafka topic asynchronously
    /// </summary>
    /// <param name="cancellationToken">Cancellation token to stop consuming</param>
    /// <returns>The consumed message as a string, or null if no message is available</returns>
    Task<string?> ConsumeAsync(CancellationToken cancellationToken);
}