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
        services.AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddIdentityCookies();
        services.AddAuthorizationBuilder();

        services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<IdentityRelevantDbContext>()
            .AddApiEndpoints();

        return services;
    }

    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder services)
    {
        services.AddNpgsqlDbContext<IdentityRelevantDbContext>(ServiceConstants.DatabaseName);
        //services.AddNpgsqlDbContext<PrimaryDbContext>(ServiceConstants.DatabaseName);

        return services;
    }

    public static void MapIdentityEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapIdentityApi<User>();
    }
}
