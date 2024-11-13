using AKS.Application.Repositories;
using AKS.Domain.Entities;
using AKS.Infrastructure.Data;
using AKS.Infrastructure.Repositories;
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
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteWriteRepository>();
        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        
        return services;
    }

    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder services)
    {
        services.AddNpgsqlDbContext<PrimaryDbContext>(ServiceConstants.PostgresDbConnection);
        return services;
    }
}
