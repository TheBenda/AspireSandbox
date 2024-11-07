using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.Domain.Entities;

public class Topping
{
    [Key]
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
}
