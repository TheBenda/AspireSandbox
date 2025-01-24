namespace AKS.Application.UseCases.BattleGroupUnitEquipment.Create;

public record CreateBattleGroupEquipment(
    Guid BattleGroupId,
    Guid BattleGroupUnitId,
    Guid EquipmentId);