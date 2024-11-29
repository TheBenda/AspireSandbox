namespace AKS.Application.UseCases.OrderItems.Create;

public record CreateOrderItem(Guid OrderId, Guid ProductId)
{
    public static CreateOrderItem New(Guid orderId, Guid productId) 
    => new CreateOrderItem(orderId, productId);
}