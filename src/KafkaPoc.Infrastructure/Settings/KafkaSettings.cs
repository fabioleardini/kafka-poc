namespace KafkaPoc.Infrastructure.Settings;

public class KafkaSettings
{
    public string BootstrapServers { get; set; } = string.Empty;
    public string GroupId { get; set; } = string.Empty;
    public string TopicName { get; set; } = string.Empty;
    public string AutoOffsetReset { get; set; } = "earliest";
    public bool EnableAutoCommit { get; set; } = true;
    public int SessionTimeoutMs { get; set; } = 45000;
}