using PS.Domain.Sagas;

namespace PS.Domain.Aggregates;

public class OrderAggregate
{
    public Guid OrderId {get; set;}
    public OrderProcessStatus Status {get; set;}
    public IEnumerable<Product> Products { get; set; } = new List<Product>();
}