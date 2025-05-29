using KafkaPoc.Domain.Entities;

namespace KafkaPoc.Infrastructure.Interfaces
{
    public interface IMessageProducer
    {
        Task ProduceAsync(StatementLineImporterMessage message, CancellationToken cancellationToken = default);
    }
}