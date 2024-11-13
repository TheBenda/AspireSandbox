var builder = DistributedApplication.CreateBuilder(args);

var dataSource = builder.AddPostgres("PostgresDbConnection")
    .WithDataVolume()
    .AddDatabase("DataSourceDb");

var messagingName = "RabbitMqMessagingConnection";
var rabbitMq = builder.AddRabbitMQ(messagingName)
    .WithManagementPlugin()
    .WithDataVolume();

var keycloak = builder.AddKeycloak("keycloak", 8080)
    .WithDataVolume();

builder.AddProject<Projects.AKS_Presentation>("mainBackend")
    .WithReference(dataSource)
    .WithReference(rabbitMq)
    .WithReference(keycloak);

builder.AddProject<Projects.PS_Presentation>("processingBackend")
    .WithReference(dataSource)
    .WithReference(rabbitMq)
    .WithReference(keycloak);

builder.AddProject<Projects.AKS_DbManager>("DbManager").WithReference(dataSource);

builder.Build().Run();
