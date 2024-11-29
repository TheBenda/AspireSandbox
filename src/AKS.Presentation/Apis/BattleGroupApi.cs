using AKS.Application.UseCases.BattleGroups.Create;
using AKS.Application.UseCases.BattleGroups.Delete;
using AKS.Application.UseCases.BattleGroupUnits.Create;
using AKS.Domain.Entities;
using AKS.Domain.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Wolverine;
using Wolverine.Runtime;

using Guid = System.Guid;

namespace AKS.Presentation.Apis;

public static class BattleGroupApi
{
 public static void MapOrderEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/BattleGroup").WithTags(nameof(BattleGroup))
            .WithDisplayName("Endoints to create, modify or delete an BattleGroup of a customer.");

        group.MapPost("/{ownerId:guid}", async (Guid ownerId, CreateBattleGroupRequest request, IMessageBus messageBus, CancellationToken cancellationToken) =>
            {
                var command = new CreateBattleGroup(ownerId, request.GroupName);
                var createdCustomer = await messageBus.InvokeAsync<BattleGroupCreated>(command, cancellationToken);
                return TypedResults.Created($"/api/Customer/", createdCustomer);
            })
            .WithName("CreateEmptyBattleGroup")
            .WithOpenApi();

        group.MapPost("/{battleGroupId:guid}/add/unit/{unitId:guid}",
            async Task<Results<Ok, NotFound<ProblemDetails>>> (Guid battleGroupId, Guid unitId,
                IMessageBus messageBus,
                CancellationToken cancellationToken) =>
            {
                var result = await messageBus.InvokeAsync<BattleGroupUnitCreated>(CreateBattleGroupUnit.New(battleGroupId, unitId), cancellationToken);
                return TypedResults.NotFound(new ProblemDetails());
            });

        group.MapDelete("/{id:guid}", async Task<Results<Ok, NotFound<ProblemDetails>>> (Guid id, IMessageBus messageBus, CancellationToken cancellationToken) => 
            {
                var customerDeleted = await messageBus.InvokeAsync<OrderDeleted>(DeleteOrder.New(id), cancellationToken);
                return customerDeleted.Result.Type switch
                {
                    ResultType.Result => TypedResults.Ok(),
                    _ => TypedResults.NotFound(new ProblemDetails
                    {
                        Title = "Customer not found",
                        Detail = customerDeleted.Result.Error?.Message,
                        Status = StatusCodes.Status404NotFound
                    })
                };
            })
            .WithName("DeleteOrder")
            .WithOpenApi();
        
    }
}

public record CreateBattleGroupRequest(string GroupName);
