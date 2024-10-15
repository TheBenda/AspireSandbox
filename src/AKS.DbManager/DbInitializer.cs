
using System.Diagnostics;

using AKS.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace AKS.DbManager;

public class DbInitializer(IServiceProvider serviceProvider, ILogger<DbInitializer> logger) :
    BackgroundService
{
    public const string ActivitySourceName = "Migrations";

    private readonly ActivitySource _activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PrimaryDbContext>();

        await InitializeDatabaseAsync(dbContext, stoppingToken);
    }

    private async Task InitializeDatabaseAsync(PrimaryDbContext dbContext, CancellationToken cancellationToken)
    {
        using var activity = _activitySource.StartActivity("Initializing catalog database", ActivityKind.Client);

        var sw = Stopwatch.StartNew();

        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(dbContext.Database.MigrateAsync, cancellationToken);

        logger.LogInformation("Database initialization completed after {ElapsedMilliseconds}ms", sw.ElapsedMilliseconds);
    }
}
