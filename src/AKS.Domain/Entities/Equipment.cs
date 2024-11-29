using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.Domain.Entities;

public class Equipment
{
    [Key]
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Rule { get; init; }
    public int? Attack { get; init; }
    public required decimal Points { get; init; }
    public Guid UnitId { get; init; }
    public Unit Unit { get; init; } = null!;
}
