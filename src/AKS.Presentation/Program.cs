using AKS.Infrastructure.Extensions;
using AKS.Presentation.Extensions.Api;
using AKS.Presentation.Extensions.Wolverine;
using AKS.ServiceDefaults;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    
});
builder.AddRabbitMQClient(connectionName: ServiceConstants.RabbitMqConnection);
builder.Services.AddInfrastructureServices();

var rabbitMqConnection = builder.Configuration.GetConnectionString(ServiceConstants.RabbitMqConnection);

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
        options.WithTitle("Orders API")
            .WithTheme(ScalarTheme.Moon)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        
        options.Authentication = new ScalarAuthenticationOptions();
    });
}

app.UseHttpsRedirection();
app.MapEndpoints();

app.Run();

public sealed partial class Program;