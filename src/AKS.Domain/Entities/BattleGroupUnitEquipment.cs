using System.ComponentModel.DataAnnotations;

using AKS.Domain.Values;

namespace AKS.Domain.Entities;

public class BattleGroupUnitEquipment
{
    [Key]
    public Guid Id { get; init; }
    public Guid EquipmentId { get; init; }
    public Guid BattleGroupUnitId { get; init; }
    public BattleGroupUnit BattleGroupUnit { get; init; } = null!;
    
    public required string Name { get; init; }
    public string? Rule { get; init; }
    public int? Attack { get; init; }
    public required decimal Points { get; init; }
}