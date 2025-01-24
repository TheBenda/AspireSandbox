using AKS.Application.UseCases.BattleGroups.Transport;
using AKS.Application.UseCases.BattleGroupUnitEquipment.Transport;
using AKS.Application.UseCases.BattleGroupUnits.Transport;
using AKS.Domain.Entities;

namespace AKS.Application.Mapping.BattleGroups;

public static class BattleGroupsExtensions
{
    public static BattleGroupDto ToDto(this BattleGroup battleGroup)
        => new BattleGroupDto(
            battleGroup.Id,
            battleGroup.GroupCreated,
            battleGroup.GroupName,
            battleGroup.BattleGroupUnits.ToDtos());

    private static List<BattleGroupUnitDto> ToDtos(this ICollection<BattleGroupUnit>? battleGroupBattleGroupUnits)
        => battleGroupBattleGroupUnits is null 
            ? [] 
            : battleGroupBattleGroupUnits.Select(x => x.ToDto()).ToList();

    private static BattleGroupUnitDto ToDto(this BattleGroupUnit unit)
        => new BattleGroupUnitDto(
            unit.Id,
            unit.Name,
            unit.Rule,
            unit.Health,
            unit.Attack,
            unit.Defense,
            unit.Movement,
            unit.Range,
            unit.Accuracy,
            unit.Points,
            unit.BattleGroupUnitEquipments.ToDtos());

    private static List<BattleGroupUnitEquipmentDto> ToDtos(
        this ICollection<BattleGroupUnitEquipment>? battleGroupBattleGroupEquipments)
        => battleGroupBattleGroupEquipments is null 
            ? [] 
            : battleGroupBattleGroupEquipments.Select(x => x.ToDto()).ToList();

    private static BattleGroupUnitEquipmentDto ToDto(
        this BattleGroupUnitEquipment equipment)
        => new BattleGroupUnitEquipmentDto(
            equipment.Id,
            equipment.Name,
            equipment.Rule,
            equipment.Attack,
            equipment.Points);
}