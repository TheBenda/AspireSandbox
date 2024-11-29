using AKS.Domain.Entities;
using AKS.Domain.Results;
using AKS.Domain.Results.Status;

namespace AKS.Application.Repositories;

public interface IUnitWriteRepository
{
    Task<Unit> CreateProductAsync(Unit unit, CancellationToken cancellationToken);
    Task<PersistenceResult<SuccsefullTransaction>> DeleteProductAsync(Guid productId, CancellationToken cancellationToken);
    Task<PersistenceResult<Unit>> CreateToppingToProductAsync(Guid productId, Equipment equipment, CancellationToken cancellationToken);
    Task<PersistenceResult<SuccsefullTransaction>> DeleteToppingAsync(Guid productId, Guid toppingId, CancellationToken cancellationToken);
}