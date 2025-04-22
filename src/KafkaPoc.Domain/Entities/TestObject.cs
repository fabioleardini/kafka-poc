namespace KafkaPoc.Domain.Entities;

public class TestObject
{
    public Guid Id { get; set; }
    public EventHeader? Header { get; set; }
}

public class EventHeader
{
    public Guid CorrelationId { get; set; }

    public Guid CausationId { get; set; }

    //public IDictionary<string, string> Metadata { get; set; }
}