using AKS.ServiceDefaults;

using Marten;

using Oakton.Resources;

using PS.Presentation.Extensions.Marten;
using PS.Presentation.Extensions.Wolverine;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration; 

builder.Services
    .AddOpenApi()
    .ConfigureMarten(configuration.GetConnectionString(ServiceConstants.PostgresDbConnection)!);

builder.AddRabbitMQClient(connectionName: ServiceConstants.RabbitMqConnection);

var rabbitMqConnection = configuration.GetConnectionString(ServiceConstants.RabbitMqConnection);

builder.Host.ConfigureWolverine(new Uri(rabbitMqConnection!));

// builder.Services.AddResourceSetupOnStartup();

builder.AddServiceDefaults();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.MapOpenApi();
    app.MapScalarApiReference(options => 
    {
        options.WithTitle("Delivery API")
            .WithTheme(ScalarTheme.Moon)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseHttpsRedirection();

app.Run();
