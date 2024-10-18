using AKS.Infrastructure.Extensions;
using AKS.Presentation.Extensions.Wolverine;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.AddRabbitMQClient(connectionName: "rabbitMqMessaging");
builder.Services.AddInfrastructureServices();

var rabbitMqConnection = builder.Configuration.GetConnectionString("rabbitMqMessaging");

builder.Host.ConfigureWolverine(new Uri(rabbitMqConnection!));

builder.AddServiceDefaults();
builder.AddInfrastructure();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.MapOpenApi();
    app.MapScalarApiReference(options => 
    {
        options.WithTitle("Persons API")
            .WithTheme(ScalarTheme.Moon)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseHttpsRedirection();

app.Run();
