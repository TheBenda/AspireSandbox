using AKS.Domain.Entities;
using AKS.Domain.Results;
using AKS.Domain.Results.Status;

namespace AKS.Application.Repositories;

public interface IBattleGroupWriteRepository
{
    Task<BattleGroup> CreateAsync(BattleGroup battleGroup, CancellationToken cancellationToken);
    Task<PersistenceResult<BattleGroup>> AddUnitToBattleGroupAsync(Guid battleGroupId, Guid unitId, CancellationToken cancellationToken);
    Task<PersistenceResult<BattleGroup>> AddToppingToProductsOrderAsync(Guid battleGroupId, Guid unitId, Guid equipmentId, CancellationToken cancellationToken);
    Task<PersistenceResult<SuccsefullTransaction>> DeleteAsync(Guid orderId, CancellationToken cancellationToken);
    Task<PersistenceResult<SuccsefullTransaction>> DelelteProductFromOrderAsync(Guid orderId, Guid orderItemId, CancellationToken cancellationToken);
    Task<PersistenceResult<SuccsefullTransaction>> DeleteToppingFromProductsOrderAsync(Guid orderId, Guid orderItemId, Guid orderToppingId, CancellationToken cancellationToken);
}
