using AKS.Domain.Entities;
using AKS.Domain.Results;

namespace AKS.Application.Repositories;

public interface IOrderReadRepository
{
    Task<PersistenceResult<Order>> GetAsync(Guid orderId, CancellationToken cancellationToken);
    Task<List<Order>> GetAllAsync(CancellationToken cancellationToken);
}