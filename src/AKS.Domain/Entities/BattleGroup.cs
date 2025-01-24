using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using AKS.Domain.Values;

namespace AKS.Domain.Entities;

public class BattleGroup
{
    [Key]
    public Guid Id { get; init; }
    public required DateTime GroupCreated { get; init; }
    public required string GroupName { get; init; }
    public System.Guid CustomerId { get; init; }
    public Customer Customer { get; init; } = null!;
    public ICollection<BattleGroupUnit> BattleGroupUnits { get; init; } = null!;
}
