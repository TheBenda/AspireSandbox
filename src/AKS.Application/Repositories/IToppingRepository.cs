using AKS.Domain.Entities;
using AKS.Domain.Results;
using AKS.Domain.Results.Status;

namespace AKS.Application.Repositories;

public interface IToppingRepository
{
    Task<Topping> CreateToppingAsync(Topping topping, CancellationToken cancellationToken);
    Task<PersistenceResult<SuccsefullTransaction>> DeleteProductAsync(Guid toppingId, CancellationToken cancellationToken);
}