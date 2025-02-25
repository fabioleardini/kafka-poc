using Confluent.Kafka;
using KafkaPoc.Infrastructure.Interfaces;
using KafkaPoc.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Kafka Producer
builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection("KafkaProducer"));
builder.Services.AddSingleton<IMessageProducer, KafkaMessageProducer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();