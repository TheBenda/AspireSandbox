using AKS.Application.UseCases.Equipments.Create;
using AKS.Application.UseCases.Units.Create;
using AKS.Application.UseCases.Units.Delete;
using AKS.Application.UseCases.Units.GetAll;
using AKS.Application.UseCases.Units.GetById;
using AKS.Application.UseCases.Units.Transport;
using AKS.Domain.Results;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Wolverine;

namespace AKS.Presentation.Apis;

public static class UnitApi
{
    public static void MapUnitEndpoints(this IEndpointRouteBuilder routes)
    {
        RouteGroupBuilder group = routes.MapGroup("/api/Unit").WithTags("Unit")
            .WithSummary("Endpoints for creating a product to the possible products to choose from later.");

        group.MapPost("/", async (CreateUnit request, IMessageBus messageBus, CancellationToken cancellationToken) =>
            {
                UnitCreated createdProduct = await messageBus.InvokeAsync<UnitCreated>(request, cancellationToken);
                return TypedResults.Created($"/api/Product/{createdProduct.Id}", createdProduct);
            })
            .WithTags("CreateUnit")
            .WithOpenApi();

        group.MapDelete("/{unitId:guid}",
                async Task<Results<Ok, NotFound<ProblemDetails>>> (Guid unitId, IMessageBus messageBus,
                    CancellationToken cancellationToken) =>
                {
                    UnitDeleted productDeleted =
                        await messageBus.InvokeAsync<UnitDeleted>(DeleteUnit.New(unitId), cancellationToken);
                    return productDeleted.Result.Type switch
                    {
                        ResultType.Result => TypedResults.Ok(),
                        _ => TypedResults.NotFound(new ProblemDetails
                        {
                            Title = "Unable to delete Unit",
                            Detail = productDeleted.Result.Error?.Message,
                            Status = StatusCodes.Status404NotFound
                        })
                    };
                })
            .WithName("DeleteUnit")
            .WithOpenApi();

        group.MapGet("/",
                async Task<Results<Ok<UnitsFound>, ProblemHttpResult>> (IMessageBus messageBus,
                    CancellationToken cancellationToken) =>
                {
                    UnitsFound foundProducts =
                        await messageBus.InvokeAsync<UnitsFound>(new FindUnits(), cancellationToken);
                    return TypedResults.Ok(foundProducts);
                })
            .WithName("GetUnits")
            .WithOpenApi();


        group.MapGet("/{unitId:guid}",
                async Task<Results<Ok<UnitDto>, NotFound<ProblemDetails>>> (Guid unitId, IMessageBus messageBus,
                    CancellationToken cancellationToken) =>
                {
                    UnitFound foundProductResult =
                        await messageBus.InvokeAsync<UnitFound>(new FindUnit(unitId), cancellationToken);

                    return foundProductResult.Product.Type switch
                    {
                        ResultType.Result => TypedResults.Ok(foundProductResult.Product.Result!.Value),
                        _ => TypedResults.NotFound(new ProblemDetails
                        {
                            Title = "Unable to find Unit",
                            Detail = foundProductResult.Product.Error?.Message,
                            Status = StatusCodes.Status404NotFound
                        })
                    };
                })
            .WithName("FindUnit")
            .WithOpenApi();
    }
}

public record AddEquipmentToUnitRequest(
    string Name,
    string? Rule,
    int? Attack,
    decimal Points)
{
    public AddEquipmentToUnit ToCommand(Guid productId)
    {
        return new AddEquipmentToUnit(productId, Name, Rule, Attack, Points);
    }
}