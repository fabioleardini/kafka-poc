using KafkaPoc.Domain.Entities;

namespace KafkaPoc.Infrastructure.Interfaces
{
    public interface IMessageProducer
    {
        Task ProduceAsync(TestObject message, CancellationToken cancellationToken = default);
    }
}