var builder = DistributedApplication.CreateBuilder(args);

var dataSource = builder.AddPostgres("dataSource")
    .WithDataVolume()
    .AddDatabase("DataSourceDb");

var messagingName = "rabbitMqMessaging";
var rabbitMq = builder.AddRabbitMQ(messagingName)
    .WithManagementPlugin();

var keycloak = builder.AddKeycloak("keycloak", 8080);

builder.AddProject<Projects.AKS_Presentation>("Presentation")
    .WithReference(dataSource)
    .WithReference(rabbitMq)
    .WithReference(keycloak);

builder.AddProject<Projects.AKS_DbManager>("DbManager").WithReference(dataSource);

builder.Build().Run();
