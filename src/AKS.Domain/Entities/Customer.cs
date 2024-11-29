using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using AKS.Domain.Values;


namespace AKS.Domain.Entities;

public class Customer
{
    [Key]
    public System.Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public Address? Address { get; init; }


    public ICollection<BattleGroup> Orders { get; init; } = null!;
}
