using AKS.Application.UseCases.BattleGroups.Create;
using AKS.Application.UseCases.BattleGroups.Delete;
using AKS.Application.UseCases.BattleGroups.Transport;
using AKS.Application.UseCases.BattleGroupUnitEquipment.Create;
using AKS.Application.UseCases.BattleGroupUnits.Create;
using AKS.Domain.Entities;
using AKS.Domain.Results;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Wolverine;

namespace AKS.Presentation.Apis;

public static class BattleGroupApi
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder routes)
    {
        RouteGroupBuilder group = routes.MapGroup("/api/BattleGroup").WithTags(nameof(BattleGroup))
            .WithDisplayName("Endpoints to create, modify or delete an BattleGroup of a customer.");

        group.MapPost("/{ownerId:guid}", async (Guid ownerId, CreateBattleGroupRequest request, IMessageBus messageBus,
                CancellationToken cancellationToken) =>
            {
                CreateBattleGroup command = new(ownerId, request.GroupName);
                BattleGroupCreated createdCustomer =
                    await messageBus.InvokeAsync<BattleGroupCreated>(command, cancellationToken);
                return TypedResults.Created("/api/Customer/", createdCustomer);
            })
            .WithName("CreateEmptyBattleGroup")
            .WithOpenApi();

        group.MapPost("/{battleGroupId:guid}/add/unit/{unitId:guid}",
                async Task<Results<Ok<BattleGroupDto>, NotFound<ProblemDetails>>> (Guid battleGroupId, Guid unitId,
                    IMessageBus messageBus,
                    CancellationToken cancellationToken) =>
                {
                    BattleGroupUnitCreated result =
                        await messageBus.InvokeAsync<BattleGroupUnitCreated>(
                            CreateBattleGroupUnit.New(battleGroupId, unitId), cancellationToken);
                    return result.BattleGroupResult.Type switch
                    {
                        ResultType.Result => TypedResults.Ok(result.BattleGroupResult.Result!.Value),
                        _ => TypedResults.NotFound(new ProblemDetails
                        {
                            Title = "Unable to create Battle Group",
                            Detail = result.BattleGroupResult.Error?.Message,
                            Status = StatusCodes.Status404NotFound
                        })
                    };
                })
            .WithName("CreateEmptyBattleGroupUnit")
            .WithOpenApi();

        group.MapPost("/{battleGroupId:guid}/add/unit/{unitId:guid}/equipment/{equipmentId:guid}",
                async Task<Results<Ok<BattleGroupDto>, NotFound<ProblemDetails>>> (
                    Guid battleGroupId, Guid unitId, Guid equipmentId,
                IMessageBus messageBus,
                CancellationToken cancellationToken) =>
            {
                BattleGroupEquipmentCreated battleGroupEquipmentCreated =
                    await messageBus.InvokeAsync<BattleGroupEquipmentCreated>(
                        new CreateBattleGroupEquipment(battleGroupId, unitId, equipmentId), cancellationToken);
                return battleGroupEquipmentCreated.Result.Type switch
                {
                    ResultType.Result => TypedResults.Ok(battleGroupEquipmentCreated.Result.Result!.Value),
                    _ => TypedResults.NotFound(new ProblemDetails
                    {
                        Title = "Unable to create Equipment for Battle Group",
                        Detail = battleGroupEquipmentCreated.Result.Error?.Message,
                        Status = StatusCodes.Status404NotFound
                    })
                };
            })
            .WithName("CreateEmptyBattleGroupEquipment")
            .WithOpenApi();

        group.MapDelete("/{id:guid}",
                async Task<Results<Ok, NotFound<ProblemDetails>>> (Guid id, IMessageBus messageBus,
                    CancellationToken cancellationToken) =>
                {
                    BattleGroupDeleted battleGroupDeleted =
                        await messageBus.InvokeAsync<BattleGroupDeleted>(DeleteBattleGroup.New(id), cancellationToken);
                    return battleGroupDeleted.Result.Type switch
                    {
                        ResultType.Result => TypedResults.Ok(),
                        _ => TypedResults.NotFound(new ProblemDetails
                        {
                            Title = "Unable to delete Battle Group",
                            Detail = battleGroupDeleted.Result.Error?.Message,
                            Status = StatusCodes.Status404NotFound
                        })
                    };
                })
            .WithName("DeleteBattleGroup")
            .WithOpenApi();
    }
}

public record CreateBattleGroupRequest(string GroupName);