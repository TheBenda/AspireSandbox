using System.ComponentModel.DataAnnotations;

using AKS.Domain.Values;

namespace AKS.Domain.Entities;

public class BattleGroupUnit
{
    [Key]
    public Guid Id { get; init; }
    public Guid UnitId { get; init; }
    public Guid BattleGroupId { get; set; }
    public BattleGroup BattleGroup { get; set; } = null!;
    public required string Name { get; init; }
    public string? Rule { get; init; }
    public required int Health { get; init; }
    public required int Attack { get; init; }
    public required int Defense { get; init; }
    public required int Movement { get; init; }
    public required decimal Range { get; init; }
    public required int Accuracy { get; init; }
    public required decimal Points { get; init; }
    public ICollection<BattleGroupUnitEquipment> BattleGroupUnitEquipments { get; init; } = null!;
}