namespace AKS.Application.UseCases.Orders.Delete;

public record DeleteOrder(Guid OrderId)
{
    public static DeleteOrder New(Guid orderId) => new(orderId);
}