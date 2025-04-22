using Confluent.Kafka;
using System.Text.Json;

namespace KafkaPoc.Domain.Entities;

public class TestObjectSerializer : ISerializer<TestObject>
{
    public byte[] Serialize(TestObject data, SerializationContext context)
    {
        return JsonSerializer.SerializeToUtf8Bytes(data);
    }
}