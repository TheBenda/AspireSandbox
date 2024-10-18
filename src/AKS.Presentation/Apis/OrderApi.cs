using AKS.Domain.Entities;
using AKS.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AKS.Presentation.Apis;

public static class OrderApi
{
 public static void MapOrderEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Order").WithTags(nameof(Order));

        // Create Order
        // Delete Order
    }
}
