using System.ComponentModel.DataAnnotations;

using AKS.Domain.Values;

namespace AKS.Domain.Entities;

public class OrderToppingItem
{
    [Key]
    public Guid Id { get; init; }
    public Guid ToppingId { get; init; }
    public Guid OrderItemId { get; init; }
    public OrderItem OrderItem { get; init; } = null!;
    
    public required string Name { get; init; }
    public required int Quantity { get; init; }
    public required decimal Price { get; init; }
}