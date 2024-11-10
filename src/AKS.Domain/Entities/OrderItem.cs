using System.ComponentModel.DataAnnotations;

using AKS.Domain.Values;

namespace AKS.Domain.Entities;

public class OrderItem
{
    [Key]
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public required string Name { get; init; }
    public required int Quantity { get; init; }
    public required decimal Price { get; init; }
    public ICollection<OrderToppingItem> OrderToppingItems { get; init; } = null!;
}