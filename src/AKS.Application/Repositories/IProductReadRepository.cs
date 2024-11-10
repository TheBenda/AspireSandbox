using AKS.Domain.Entities;
using AKS.Domain.Results;

namespace AKS.Application.Repositories;

public interface IProductReadRepository
{
    Task<PersistenceResult<Product>> GetProductByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Product>> GetAllProductsAsync(CancellationToken cancellationToken);
}