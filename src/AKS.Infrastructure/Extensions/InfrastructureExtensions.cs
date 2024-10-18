using AKS.Domain.Entities;
using AKS.Infrastructure.Data;
using AKS.ServiceDefaults;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AKS.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        return services;
    }

    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder services)
    {
        services.AddNpgsqlDbContext<PrimaryDbContext>(ServiceConstants.DatabaseName);

        return services;
    }
}
