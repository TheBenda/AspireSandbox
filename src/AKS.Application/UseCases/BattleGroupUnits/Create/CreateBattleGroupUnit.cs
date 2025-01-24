namespace AKS.Application.UseCases.BattleGroupUnits.Create;

public record CreateBattleGroupUnit(Guid BattleGroupId, Guid UnitId)
{
    public static CreateBattleGroupUnit New(Guid battleGroupId, Guid unitId) 
    => new CreateBattleGroupUnit(battleGroupId, unitId);
}