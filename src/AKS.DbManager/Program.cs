using AKS.DbManager;
using AKS.Infrastructure.Data;
using AKS.ServiceDefaults;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<PrimaryDbContext>(ServiceConstants.DatabaseName, null,
    optionsBuilder => optionsBuilder.UseNpgsql(npgsqlBuilder =>
        npgsqlBuilder.MigrationsAssembly(typeof(Program).Assembly.GetName().Name)));

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(DbInitializer.ActivitySourceName));

builder.Services.AddSingleton<DbInitializer>();
builder.Services.AddHostedService(sp => sp.GetRequiredService<DbInitializer>());
builder.Services.AddHealthChecks()
    .AddCheck<DbInitializerHealthCheck>("DbInitializer", null);

var app = builder.Build();

app.MapDefaultEndpoints();

app.Run();
