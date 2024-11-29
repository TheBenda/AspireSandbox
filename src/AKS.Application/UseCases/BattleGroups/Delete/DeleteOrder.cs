namespace AKS.Application.UseCases.BattleGroups.Delete;

public record DeleteOrder(Guid OrderId)
{
    public static DeleteOrder New(Guid orderId) => new(orderId);
}