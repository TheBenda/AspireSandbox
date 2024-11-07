using AKS.Domain.Entities;

namespace AKS.Application.Repositories;

public interface IOrderRepository
{
    Task<Order> CreateAsync(Order order, CancellationToken cancellationToken);
}
