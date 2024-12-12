var builder = DistributedApplication.CreateBuilder(args);

var kafka = builder.AddKafka("kafka")
    .WithKafkaUI()
    .WithLifetime(ContainerLifetime.Session);

var consumer = builder.AddProject<Projects.AspireWithKafka_Consumer>("consumer")
    .WithReference(kafka)
    .WaitFor(kafka);

var producer = builder.AddProject<Projects.AspireWithKafka_Producer>("producer")
    .WithReference(consumer)
    .WaitFor(consumer)
    .WithReference(kafka)
    .WaitFor(kafka);

builder.Build().Run();