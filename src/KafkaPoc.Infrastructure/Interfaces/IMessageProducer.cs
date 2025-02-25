using KafkaPoc.Domain.Entities;

namespace KafkaPoc.Infrastructure.Interfaces
{
    public interface IMessageProducer
    {
        Task ProduceAsync(Message message, CancellationToken cancellationToken = default);
    }
}