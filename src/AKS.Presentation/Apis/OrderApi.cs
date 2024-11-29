using AKS.Application.UseCases.OrderItems.Create;
using AKS.Application.UseCases.Orders.Create;
using AKS.Application.UseCases.Orders.Delete;
using AKS.Domain.Entities;
using AKS.Domain.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Wolverine;
using Wolverine.Runtime;

namespace AKS.Presentation.Apis;

public static class OrderApi
{
 public static void MapOrderEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Order").WithTags(nameof(Order))
            .WithDisplayName("Endoints to create, modify or delete an order of a customer.");

        group.MapPost("/", async (CreateOrder createOrder, IMessageBus messageBus, CancellationToken cancellationToken) =>
            {
                var createdCustomer = await messageBus.InvokeAsync<OrderCreated>(createOrder, cancellationToken);
                return TypedResults.Created($"/api/Customer/",createdCustomer);
            })
            .WithName("CreateEmptyOrder")
            .WithOpenApi();

        group.MapPost("/{orderId:guid}/add/product/{productId:guid}",
            async Task<Results<Ok, NotFound<ProblemDetails>>> (Guid orderId, Guid productId,
                IMessageBus messageBus,
                CancellationToken cancellationToken) =>
            {
                var result = await messageBus.InvokeAsync<OrderItemCreated>(CreateOrderItem.New(orderId, productId), cancellationToken);
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
