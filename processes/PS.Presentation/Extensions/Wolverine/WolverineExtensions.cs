using AKS.MessagingContracts;
using PS.Infrastructure.Extensions;
using JasperFx.Core;

using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.RabbitMQ;

namespace PS.Presentation.Extensions.Wolverine;

internal static class WolverineExtensions
{
    internal static ConfigureHostBuilder ConfigureWolverine(this ConfigureHostBuilder hostBuilder, Uri connectionString)
    {
        hostBuilder.UseWolverine(options =>
            {
                options.Policies
                    .OnAnyException()
                    .RetryWithCooldown(50.Milliseconds(), 100.Milliseconds(), 250.Milliseconds());
                
                options.ListenToRabbitQueue(MessagingConstants.ProcessesQueue).UseForReplies();

                options.Services.AddMartenInfrastructureServices();

                options
                    .UseRabbitMq(connectionString)
                    .AutoProvision();

                options.Policies.AutoApplyTransactions();
            });
        return hostBuilder;
    }
}
