using AKS.MessagingContracts;
using AKS.MessagingContracts.Command;

using Wolverine;
using Wolverine.RabbitMQ;

namespace AKS.Presentation.Extensions.Wolverine;

internal static class WolverineExtensions
{
    internal static ConfigureHostBuilder ConfigureWolverine(this ConfigureHostBuilder hostBuilder, Uri connectionString)
    {
        hostBuilder.UseWolverine(options =>
            {
                options
                    .PublishMessage<PrepareProcessingSaga>()
                    .ToRabbitExchange(MessagingConstants.ProcessingExchange, exchange => {
                        exchange.ExchangeType = ExchangeType.Direct;
                        exchange.BindQueue(MessagingConstants.ProcessesQueue);
                    });
 
                options
                    .UseRabbitMq(connectionString)
                    .AutoProvision();
            });
        return hostBuilder;
    }
}
