namespace AKS.Application.UseCases.BattleGroups.Delete;

public record DeleteBattleGroup(Guid BattleGroupId)
{
    public static DeleteBattleGroup New(Guid orderId)
    {
        return new DeleteBattleGroup(orderId);
    }
}