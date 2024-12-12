using Confluent.Kafka;
using KafkaAspire.Consumer;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddKafkaConsumer<string, string>("kafka", options =>
{
    options.Config.GroupId = "messaging-group";
});

builder.Services.AddHostedService<Worker>();

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();