using AKS.Domain.Entities;
using AKS.Domain.Results;
using AKS.Domain.Results.Status;

namespace AKS.Application.Repositories;

public interface IBattleGroupWriteRepository
{
    Task<BattleGroup> CreateAsync(BattleGroup battleGroup, CancellationToken cancellationToken);
    Task<PersistenceResult<BattleGroup>> AddProductToOrderAsync(Guid orderId, Guid productId, CancellationToken cancellationToken);
    Task<PersistenceResult<BattleGroup>> AddToppingToProductsOrderAsync(Guid orderId, Guid orderItemId, BattleGroupUnitEquipment battleGroupUnitEquipment, CancellationToken cancellationToken);
    Task<PersistenceResult<SuccsefullTransaction>> DeleteAsync(Guid orderId, CancellationToken cancellationToken);
    Task<PersistenceResult<SuccsefullTransaction>> DelelteProductFromOrderAsync(Guid orderId, Guid orderItemId, CancellationToken cancellationToken);
    Task<PersistenceResult<SuccsefullTransaction>> DeleteToppingFromProductsOrderAsync(Guid orderId, Guid orderItemId, Guid orderToppingId, CancellationToken cancellationToken);
}
