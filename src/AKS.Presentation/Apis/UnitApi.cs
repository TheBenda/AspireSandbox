using AKS.Application.UseCases.Customers.Create;
using AKS.Application.UseCases.Equipments.Create;
using AKS.Application.UseCases.Equipments.Delete;
using AKS.Application.UseCases.Units.Create;
using AKS.Application.UseCases.Units.Delete;
using AKS.Application.UseCases.Units.GetAll;
using AKS.Application.UseCases.Units.GetById;
using AKS.Application.UseCases.Units.Transport;
using AKS.Domain.Entities;
using AKS.Domain.Results;
using AKS.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Wolverine;

using Guid = System.Guid;

namespace AKS.Presentation.Apis;

public static class UnitApi
{
    public static void MapUnitEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Unit").WithTags("Unit")
            .WithSummary("Endpoints for creating a product to the possible products to choose from later.");

        // Create Product
        group.MapPost("/", async (CreateUnit request, IMessageBus messageBus, CancellationToken cancellationToken) =>
        {
            var createdProduct = await messageBus.InvokeAsync<UnitCreated>(request, cancellationToken);
            return TypedResults.Created($"/api/Product/{createdProduct.Id}",createdProduct);
        });
        // Remove Product
        group.MapDelete("/{id}", async Task<Results<Ok, NotFound<ProblemDetails>>> (Guid id, IMessageBus messageBus, CancellationToken cancellationToken) => 
            {
                var productDeleted = await messageBus.InvokeAsync<UnitDeleted>(DeleteUnit.New(id), cancellationToken);
                return productDeleted.Result.Type switch
                {
                    ResultType.Result => TypedResults.Ok(),
                    _ => TypedResults.NotFound(new ProblemDetails
                    {
                        Title = "Product not found",
                        Detail = productDeleted.Result.Error?.Message,
                        Status = StatusCodes.Status404NotFound
                    })
                };
            })
            .WithName("DeleteProduct")
            .WithOpenApi();
        // Get All Products
        group.MapGet("/",
            async Task<Results<Ok<UnitsFound>, ProblemHttpResult>> (IMessageBus messageBus,
                CancellationToken cancellationToken) =>
            {
                var foundProducts = await messageBus.InvokeAsync<UnitsFound>(new FindUnits(), cancellationToken);
                return TypedResults.Ok(foundProducts);
            });
        
        // Get Products with its topping options.
        group.MapGet("/{productId}",
            async Task<Results<Ok<UnitDto>, NotFound<ProblemDetails>>> (Guid productId, IMessageBus messageBus,
                CancellationToken cancellationToken) =>
            {
                var foundProductResult = await messageBus.InvokeAsync<UnitFound>(new FindUnit(productId), cancellationToken);
                
                return foundProductResult.Product.Type switch
                {
                    ResultType.Result => TypedResults.Ok(foundProductResult.Product.Result!.Value),
                    _ => TypedResults.NotFound(new ProblemDetails
                    {
                        Title = "Product not found",
                        Detail = foundProductResult.Product.Error?.Message,
                        Status = StatusCodes.Status404NotFound
                    })
                };
            });

        var toppingGroup = routes.MapGroup("/api/Topping").WithTags(nameof(Unit))
            .WithSummary("Endpoints to add toppings possible to add to a product.");
        
        // Add Toppings to Product.
        toppingGroup.MapPost("/{productId}",
            async Task<Results<Created<UnitDto>, NotFound<ProblemDetails>>> (Guid productId, AddToppingToProductRequest request, IMessageBus messageBus,
                CancellationToken cancellationToken) =>
            {
                var addedToppingResult = await messageBus.InvokeAsync<AddedEquipmentToUnit>(request.ToCommand(productId), cancellationToken);

                return addedToppingResult.Result.Type switch
                {
                    ResultType.Result => TypedResults.Created($"/api/Topping/{addedToppingResult.Result.Result!.Value.Id}",addedToppingResult.Result.Result!.Value),
                    _ => TypedResults.NotFound(new ProblemDetails
                    {
                        Title = "Topping not found",
                        Detail = addedToppingResult.Result.Error?.Message,
                        Status = StatusCodes.Status404NotFound
                    })
                };
            });
        // Remove Toppings from Product.
        toppingGroup.MapDelete("/{productId}/{toppingId}", async Task<Results<Ok, NotFound<ProblemDetails>>> (Guid productId, Guid toppingId, IMessageBus messageBus, CancellationToken cancellationToken) => 
            {
                var toppingDeleted = await messageBus.InvokeAsync<EquipmentDeleted>(DeleteEquipment.New(productId, toppingId), cancellationToken);
                return toppingDeleted.Result.Type switch
                {
                    ResultType.Result => TypedResults.Ok(),
                    _ => TypedResults.NotFound(new ProblemDetails
                    {
                        Title = "Topping not found",
                        Detail = toppingDeleted.Result.Error?.Message,
                        Status = StatusCodes.Status404NotFound
                    })
                };
            })
            .WithName("DeleteTopping")
            .WithOpenApi();
    }
}

public record AddToppingToProductRequest(
    string Name,
    string? Rule,
    int? Attack,
    decimal Points)
{
    public AddEquipmentToUnit ToCommand(Guid productId) => new AddEquipmentToUnit(productId, Name, Rule, Attack, Points);
}
