using Marten;

using PS.Presentation.Extensions.Marten;
using PS.Presentation.Extensions.Wolverine;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration; 

builder.Services.AddOpenApi().ConfigureMarten(configuration.GetConnectionString("dataSource")!);
builder.AddRabbitMQClient(connectionName: "rabbitMqMessaging");

var rabbitMqConnection = configuration.GetConnectionString("rabbitMqMessaging");

builder.Host.ConfigureWolverine(new Uri(rabbitMqConnection!));

builder.AddServiceDefaults();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
