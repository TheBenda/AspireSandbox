using AKS.Application.UseCases.BattleGroupUnitEquipment.Transport;

namespace AKS.Application.UseCases.BattleGroupUnits.Transport;

public record BattleGroupUnitDto(
    Guid Id,
    string Name,
    string? Rule,
    int Health,
    int Attack,
    int Defense,
    int Movement,
    decimal Range,
    int Accuracy,
    decimal Points,
    List<BattleGroupUnitEquipmentDto> Equipments);