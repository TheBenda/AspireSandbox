using AKS.Domain.Entities;
using AKS.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AKS.Presentation.Apis;

public static class ProductApi
{
    public static void MapProductEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Product").WithTags(nameof(Product));

        // Create Product to Order
        // Remove Product from Oder
        // Add Toppings to Product
    }
}
