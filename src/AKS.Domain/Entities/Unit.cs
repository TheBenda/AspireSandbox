using System.ComponentModel.DataAnnotations;

namespace AKS.Domain.Entities;

public class Unit
{
    [Key]
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Rule { get; init; }
    public required int Health { get; init; }
    public required int Attack { get; init; }
    public required int Defense { get; init; }
    public required int Movement { get; init; }
    public required decimal Range { get; init; }
    public required int Accuracy { get; init; }
    public required decimal Points { get; init; }

    public ICollection<Equipment> Equipments { get; init; } = null!;
}
