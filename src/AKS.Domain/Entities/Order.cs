using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using AKS.Domain.Values;

namespace AKS.Domain.Entities;

public class Order
{
    [Key]
    public Guid Id { get; init; }
    public required DateTime OrderPlaced { get; init; }
    public required Address ShipmentAddress { get; set; }
    public DateTime? OrderFulfilled { get; init; }
    public DateTime? OrderPayed { get; init; }
    public DateTime? DeliveryStated { get; init; }
    public DateTime? DeliveryCompleted { get; init; }
    public Guid CustomerId { get; init; }
    public Customer Customer { get; init; } = null!;
    public ICollection<OrderItem> OrderItems { get; init; } = null!;
}
