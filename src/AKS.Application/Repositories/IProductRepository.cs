using AKS.Domain.Entities;
using AKS.Domain.Results;
using AKS.Domain.Results.Status;

namespace AKS.Application.Repositories;

public interface IProductRepository
{
    Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken);
    Task<PersistenceResult<Product>> GetProductByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<PersistenceResult<SuccsefullTransaction>> DeleteProductAsync(Guid productId, CancellationToken cancellationToken);
    Task<List<Product>> GetAllProductsAsync(CancellationToken cancellationToken);
}
