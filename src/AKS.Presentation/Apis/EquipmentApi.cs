using AKS.Application.UseCases.Equipments.Create;
using AKS.Application.UseCases.Equipments.Delete;
using AKS.Application.UseCases.Units.Transport;
using AKS.Domain.Results;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Wolverine;

namespace AKS.Presentation.Apis;

public static class EquipmentApi
{
    public static void MapEquipmentEndpoints(this IEndpointRouteBuilder routes)
    {
        RouteGroupBuilder? toppingGroup = routes.MapGroup("/api/equipments")
            .WithTags("Equipment")
            .WithSummary("Endpoints to add equipments to a unit.");

        toppingGroup.MapPost("/{unitId:guid}",
                async Task<Results<Created<UnitDto>, NotFound<ProblemDetails>>> (Guid unitId,
                    AddEquipmentToUnitRequest request, IMessageBus messageBus,
                    CancellationToken cancellationToken) =>
                {
                    AddedEquipmentToUnit? addedToppingResult =
                        await messageBus.InvokeAsync<AddedEquipmentToUnit>(request.ToCommand(unitId),
                            cancellationToken);

                    return addedToppingResult.Result.Type switch
                    {
                        ResultType.Result => TypedResults.Created(
                            $"/api/unit/{addedToppingResult.Result.Result!.Value.Id}",
                            addedToppingResult.Result.Result!.Value),
                        _ => TypedResults.NotFound(new ProblemDetails
                        {
                            Title = "Unable to add Equipment to Unit",
                            Detail = addedToppingResult.Result.Error?.Message,
                            Status = StatusCodes.Status404NotFound
                        })
                    };
                })
            .WithName("AddEquipmentToUnit")
            .WithOpenApi();

        toppingGroup.MapDelete("/{unitId:guid}/{equipmentId:guid}",
                async Task<Results<Ok, NotFound<ProblemDetails>>> (Guid unitId, Guid equipmentId,
                    IMessageBus messageBus, CancellationToken cancellationToken) =>
                {
                    EquipmentDeleted? toppingDeleted =
                        await messageBus.InvokeAsync<EquipmentDeleted>(DeleteEquipment.New(unitId, equipmentId),
                            cancellationToken);
                    return toppingDeleted.Result.Type switch
                    {
                        ResultType.Result => TypedResults.Ok(),
                        _ => TypedResults.NotFound(new ProblemDetails
                        {
                            Title = "Unable to delete Equipment",
                            Detail = toppingDeleted.Result.Error?.Message,
                            Status = StatusCodes.Status404NotFound
                        })
                    };
                })
            .WithName("DeleteEquipment")
            .WithOpenApi();
    }
}