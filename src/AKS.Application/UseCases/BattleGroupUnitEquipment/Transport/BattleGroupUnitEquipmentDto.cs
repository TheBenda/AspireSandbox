namespace AKS.Application.UseCases.BattleGroupUnitEquipment.Transport;

public record BattleGroupUnitEquipmentDto(
    Guid Id,
    string Name,
    string? Rule,
    int? Attack,
    decimal Points);