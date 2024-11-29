using AKS.Domain.Entities;
using AKS.Domain.Results;

using Guid = System.Guid;

namespace AKS.Application.Repositories;

public interface IUnitReadRepository
{
    Task<PersistenceResult<Unit>> GetProductByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Unit>> GetAllProductsAsync(CancellationToken cancellationToken);
}