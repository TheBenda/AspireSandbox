using AKS.Application.UseCases.Customers.Create;
using AKS.Application.UseCases.Products.Create;
using AKS.Application.UseCases.Products.Delete;
using AKS.Application.UseCases.Products.GetAll;
using AKS.Application.UseCases.Products.GetById;
using AKS.Application.UseCases.Toppings.Create;
using AKS.Application.UseCases.Toppings.Delete;
using AKS.Domain.Entities;
using AKS.Domain.Results;
using AKS.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Wolverine;

namespace AKS.Presentation.Apis;

public static class ProductApi
{
    public static void MapProductEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Product").WithTags(nameof(Product))
            .WithSummary("Endpoints for creating a product to the possible products to choose from later.");

        // Create Product
        group.MapPost("/", async (CreateCustomer request, IMessageBus messageBus, CancellationToken cancellationToken) =>
        {
            
        });
        // Remove Product
        group.MapDelete("/{id}", async Task<Results<Ok, NotFound<ProblemDetails>>> (Guid id, IMessageBus messageBus, CancellationToken cancellationToken) => 
            {
                var productDeleted = await messageBus.InvokeAsync<ProductDeleted>(DeleteProduct.New(id), cancellationToken);
                return productDeleted.Result.Type switch
                {
                    ResultType.Result => TypedResults.Ok(),
                    _ => TypedResults.NotFound(new ProblemDetails
                    {
                        Title = "Customer not found",
                        Detail = productDeleted.Result.Error?.Message,
                        Status = StatusCodes.Status404NotFound
                    })
                };
            })
            .WithName("DeleteCustomer")
            .WithOpenApi();
        // Get All Products
        group.MapGet("/",
            async Task<Results<Ok<ProductsFound>, NotFound<ProblemDetails>>> (IMessageBus messageBus,
                CancellationToken cancellationToken) =>
            {
                return TypedResults.NotFound(new ProblemDetails());
            });
        
        // Get Products with its topping options.
        group.MapGet("/{productId}",
            async Task<Results<Ok<ProductFound>, NotFound<ProblemDetails>>> (Guid productId, IMessageBus messageBus,
                CancellationToken cancellationToken) =>
            {
                return TypedResults.NotFound(new ProblemDetails());
            });

        var toppingGroup = routes.MapGroup("/api/Topping").WithTags(nameof(Product))
            .WithSummary("Endpoints to add toppings possible to add to a product.");
        
        // Add Toppings to Product.
        toppingGroup.MapPost("/{productId}",
            async (Guid productId, AddToppingToProduct request, IMessageBus messageBus,
                CancellationToken cancellationToken) =>
            {
                
            });
        // Remove Toppings from Product.
        toppingGroup.MapDelete("/{id}", async Task<Results<Ok, NotFound<ProblemDetails>>> (Guid id, IMessageBus messageBus, CancellationToken cancellationToken) => 
            {
                var toppingDeleted = await messageBus.InvokeAsync<ToppingDeleted>(DeleteTopping.New(id), cancellationToken);
                return toppingDeleted.Result.Type switch
                {
                    ResultType.Result => TypedResults.Ok(),
                    _ => TypedResults.NotFound(new ProblemDetails
                    {
                        Title = "Customer not found",
                        Detail = toppingDeleted.Result.Error?.Message,
                        Status = StatusCodes.Status404NotFound
                    })
                };
            })
            .WithName("DeleteCustomer")
            .WithOpenApi();
    }
}
