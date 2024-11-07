using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.Domain.Entities;

public class Product
{
    [Key]
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }

    public ICollection<Topping> Toppings { get; init; } = null!;
}
