using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AKS.Domain.Entities;

public class Topping
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
}
