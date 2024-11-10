using AKS.Application.UseCases.Orders.Pay;

using Wolverine;

namespace AKS.Presentation.Apis;

public static class PaymentApi
{
    public static void MapPaymentEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Payment").WithTags("Payment")
            .WithDisplayName("Endpoints regarding finishing a order, by paying it.");
        
        group.MapPost("/", async (PayOrder payOrder, IMessageBus messageBus, CancellationToken cancellationToken) =>
            {
                await messageBus.InvokeAsync(payOrder, cancellationToken);
                return TypedResults.Ok();
            })
            .WithName("Pay Order")
            .WithTags("Pay a order to start its delivery")
            .WithOpenApi();
    }
}