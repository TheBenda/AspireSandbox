using AKS.Infrastructure.Data;
using AKS.ServiceDefaults;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Testcontainers.PostgreSql;
using Testcontainers.RabbitMq;

using TUnit.Core.Interfaces;

using Wolverine;

namespace AKS.IntegrationTests;

public class BaseIntegrationTest : IAsyncInitializer, IAsyncDisposable
{
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
        .WithUsername("postgres")
        .WithPassword("postgres")
        .WithDatabase("postgres")
        .Build();

    private readonly RabbitMqContainer _rabbitMqContainer = new RabbitMqBuilder()
        .Build();
    
    private WebApplicationFactory<Program> _webApplicationFactory = default!;
    public IMessageBus MessageBus = default!;
    
    public const string AdminToken = nameof(AdminToken);
    public const string UserToken = nameof(UserToken);

    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();
        await _rabbitMqContainer.StartAsync();

        _webApplicationFactory = new TestWebApplicationFactory(_postgreSqlContainer.GetConnectionString(), _rabbitMqContainer.GetConnectionString());
        
        // ensure server started
        _ = _webApplicationFactory.Server;
        
        using var serviceScope = _webApplicationFactory.Services.CreateScope();

        var context = serviceScope.ServiceProvider.GetRequiredService<PrimaryDbContext>();
        await context.Database.MigrateAsync();

        MessageBus = serviceScope.ServiceProvider.GetRequiredService<IMessageBus>();
    }
    

    public async ValueTask DisposeAsync()
    {
        await _webApplicationFactory.DisposeAsync();
        await _postgreSqlContainer.StopAsync();
        await _rabbitMqContainer.StopAsync();
    }
    
    private HttpClient GetHttpClient(string token)
    {
        var client = _webApplicationFactory.CreateClient();

        client.DefaultRequestHeaders.Add(
            "X-Api-Key",
            token
        );

        return client;
    }

    public HttpClient GetAdminClient() =>
        GetHttpClient(AdminToken);

    public HttpClient GetUserClient() =>
        GetHttpClient(UserToken);
}

file sealed class TestWebApplicationFactory(string connectionString, string rabbitMqConnectionString) : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        _ = builder.UseEnvironment("Testing");

        _ = builder.ConfigureHostConfiguration(
            cb => cb.AddInMemoryCollection(
                new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase)
                {
                    // ["UseSecretsJson"] = bool.FalseString,
                    // ["UseAuth0"] = bool.FalseString,
                    // ["UseHttpsRedirection"] = bool.FalseString,
                    // ["ProcessFeatureJob:Enabled"] = bool.FalseString,
                    [$"ConnectionStrings:{ServiceConstants.PostgresDbConnection}"] = connectionString,
                    [$"ConnectionStrings:{ServiceConstants.RabbitMqConnection}"] = rabbitMqConnectionString,
                }
            )
        );

        builder.ConfigureServices(services =>
        {
            var desc = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<PrimaryDbContext>));

            if (desc is not null)
            {
                services.Remove(desc);
            }

            services.AddDbContext<PrimaryDbContext>(opts => opts.UseNpgsql(connectionString));
        });

        return base.CreateHost(builder);
    }
}