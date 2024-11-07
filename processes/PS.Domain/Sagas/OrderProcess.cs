using Wolverine;

namespace PS.Domain.Sagas;

public class OrderProcess : Saga
{
    public Guid OrderProcessId { get; set; }
    public OrderProcessStatus OrderProcessStatus { get; set; }
}