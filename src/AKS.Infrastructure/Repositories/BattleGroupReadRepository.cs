using AKS.Application.Repositories;
using AKS.Domain.Entities;
using AKS.Domain.Results;
using AKS.Domain.Results.Errors;
using AKS.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace AKS.Infrastructure.Repositories;

public class BattleGroupReadRepository(PrimaryDbContext dbContext) : IBattleGroupReadRepository
{
    public async Task<PersistenceResult<BattleGroup>> GetAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var foundOrder = await dbContext.BattleGroups
            .AsNoTracking()
            .Where(o => o.Id == orderId)
            .Include(m => m.BattleGroupUnits)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (foundOrder is null)
            return PersistenceResult<BattleGroup>.Failure(NotFound.WithMessage($"Unable to find Order with id: {orderId}."));

        var orderItemsIds = foundOrder.BattleGroupUnits.Select(orderItem => orderItem.Id).ToList();

        var orderToppingItems = await dbContext.BattleGroupUnitEquipments
            .AsNoTracking()
            .Where(m => orderItemsIds.Contains(m.BattleGroupUnitId))
            .ToDictionaryAsync(key => key.BattleGroupUnitId, cancellationToken);
        
        foreach (var foundOrderOrderItem in foundOrder.BattleGroupUnits)
        {
            foundOrderOrderItem.BattleGroupUnitEquipments.Add(orderToppingItems[foundOrderOrderItem.Id]);
        }
        
        return PersistenceResult<BattleGroup>.Success(TypedResult<BattleGroup>.Of(foundOrder));
    }

    public Task<List<BattleGroup>> GetAllAsync(CancellationToken cancellationToken)
        => dbContext.BattleGroups.AsNoTracking().ToListAsync(cancellationToken);
}