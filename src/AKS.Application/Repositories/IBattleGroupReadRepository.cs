using AKS.Domain.Entities;
using AKS.Domain.Results;

namespace AKS.Application.Repositories;

public interface IBattleGroupReadRepository
{
    Task<PersistenceResult<BattleGroup>> GetAsync(Guid orderId, CancellationToken cancellationToken);
    Task<List<BattleGroup>> GetAllAsync(CancellationToken cancellationToken);
}