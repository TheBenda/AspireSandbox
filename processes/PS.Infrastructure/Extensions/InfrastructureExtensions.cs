using Microsoft.Extensions.DependencyInjection;

using PS.Application.Marten;
using PS.Infrastructure.Marten;

namespace PS.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddMartenInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IMartenRepository, MartenRepository>();
        return services;
    }


}
