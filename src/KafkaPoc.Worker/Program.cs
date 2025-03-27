using Confluent.Kafka;
using KafkaPoc.Infrastructure.Interfaces;
using KafkaPoc.Infrastructure.Services;
using KafkaPoc.Worker.Services;
using Microsoft.Extensions.Options;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<KafkaConsumerWorker>();
        services.Configure<ConsumerConfig>(hostContext.Configuration.GetSection("Kafka"));

        services.AddSingleton(sp =>
        {
            var config = sp.GetRequiredService<IOptions<ConsumerConfig>>();

            return new ConsumerBuilder<string, string>(config.Value)
                .Build();
        });
        services.AddSingleton<IKafkaConsumerService, KafkaConsumerService>();
    })
    .Build();

await host.RunAsync();