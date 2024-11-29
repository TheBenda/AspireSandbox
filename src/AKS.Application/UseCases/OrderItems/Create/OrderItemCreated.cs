using AKS.Application.UseCases.Orders.Transport;
using AKS.Domain.Entities;
using AKS.Domain.Results;

namespace AKS.Application.UseCases.OrderItems.Create;

public record OrderItemCreated(PersistenceResult<OrderDto> Order);