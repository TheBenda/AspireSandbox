using AKS.Domain.Entities;
using AKS.Domain.Results;
using AKS.Domain.Results.Status;

namespace AKS.Application.Repositories;

public interface IProductWriteRepository
{
    Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken);
    Task<PersistenceResult<SuccsefullTransaction>> DeleteProductAsync(Guid productId, CancellationToken cancellationToken);
    Task<PersistenceResult<Product>> CreateToppingToProductAsync(Guid productId, Topping topping, CancellationToken cancellationToken);
    Task<PersistenceResult<SuccsefullTransaction>> DeleteToppingAsync(Guid productId, Guid toppingId, CancellationToken cancellationToken);
}