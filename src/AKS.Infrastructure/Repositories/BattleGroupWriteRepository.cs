using AKS.Application.Repositories;
using AKS.Domain.Entities;
using AKS.Domain.Results;
using AKS.Domain.Results.Errors;
using AKS.Domain.Results.Status;
using AKS.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace AKS.Infrastructure.Repositories;

public class BattleGroupWriteRepository(PrimaryDbContext dbContext) : IBattleGroupWriteRepository
{
    public async Task<BattleGroup> CreateAsync(BattleGroup battleGroup, CancellationToken cancellationToken)
    {
        dbContext.BattleGroups.Add(battleGroup);
        await dbContext
            .SaveChangesAsync(cancellationToken);
        return battleGroup;
    }

    public async Task<PersistenceResult<BattleGroup>> AddUnitToBattleGroupAsync(Guid battleGroupId, Guid unitId, CancellationToken cancellationToken)
    {
        var foundBattleGroup = await dbContext.BattleGroups
            .Where(model => model.Id == battleGroupId)
            .Include(e => e.BattleGroupUnits)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (foundBattleGroup is null)
            return PersistenceResult<BattleGroup>.Failure(NotFound.WithMessage($"Unable to add Unit to Group, as no BattleGroup with id: {battleGroupId} was found."));
        
        var foundUnit = await dbContext.Units
            .AsNoTracking()
            .Where(model => model.Id == unitId)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (foundUnit is null)
            return PersistenceResult<BattleGroup>.Failure(NotFound.WithMessage($"Unable to add Unit to Group, as no Unit with id: {unitId} was found."));

        var battleGroupUnit = new BattleGroupUnit
        {
            Name = foundUnit.Name,
            Rule = foundUnit.Rule,
            Health = foundUnit.Health,
            Attack = foundUnit.Attack,
            Defense = foundUnit.Defense,
            Movement = foundUnit.Movement,
            Range = foundUnit.Range,
            Accuracy = foundUnit.Accuracy,
            Points = foundUnit.Points
        };
        
        foundBattleGroup.BattleGroupUnits.Add(battleGroupUnit);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return PersistenceResult<BattleGroup>.Success(TypedResult<BattleGroup>.Of(foundBattleGroup));
    }

    public async Task<PersistenceResult<BattleGroup>> AddToppingToProductsOrderAsync(Guid battleGroupId, Guid battleGroupUnitId, Guid equipmentId,
        CancellationToken cancellationToken)
    {
        var foundBattleGroup = await dbContext.BattleGroups
            .Where(model => model.Id == battleGroupId)
            .Include(e => e.BattleGroupUnits)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (foundBattleGroup is null)
            return PersistenceResult<BattleGroup>.Failure(NotFound.WithMessage($"Unable to add BattleGroupUnitEquipment, as no BattleGroup with id: {battleGroupId} was found."));
        
        var foundBattleGroupUnit = await dbContext.BattleGroupUnits
            .Where(model => model.Id == battleGroupUnitId)
            .Include(e => e.BattleGroupUnitEquipments)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (foundBattleGroupUnit is null)
            return PersistenceResult<BattleGroup>.Failure(NotFound.WithMessage($"Unable to add BattleGroupUnitEquipment, as no BattleGroupUnit with id: {battleGroupUnitId} was found."));
        
        var foundEquipment = await dbContext.Equipments
            .AsNoTracking()
            .Where(model => model.Id == equipmentId)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (foundEquipment is null)
            return PersistenceResult<BattleGroup>.Failure(NotFound.WithMessage($"Unable to add BattleGroupUnitEquipment, as no Equipment with id: {battleGroupUnitId} was found."));

        var battleGroupUnitEquipment = new BattleGroupUnitEquipment
        {
            Name = foundEquipment.Name,
            Rule = foundEquipment.Rule,
            Attack = foundEquipment.Attack,
            Points = foundEquipment.Points
        };
        foundBattleGroupUnit.BattleGroupUnitEquipments.Add(battleGroupUnitEquipment);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return PersistenceResult<BattleGroup>.Success(TypedResult<BattleGroup>.Of(foundBattleGroup));
    }

    public async Task<PersistenceResult<SuccsefullTransaction>> DeleteAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var affected = await dbContext.BattleGroups
            .Where(model => model.Id == orderId)
            .ExecuteDeleteAsync(cancellationToken);
        return affected == 1 ? 
            PersistenceResult<SuccsefullTransaction>
                .Success(TypedResult<SuccsefullTransaction>
                    .Of(new SuccsefullTransaction($"Deleted Order with id: {orderId}"))) : 
            PersistenceResult<SuccsefullTransaction>.Failure(NotFound.Empty());
    }

    public async Task<PersistenceResult<SuccsefullTransaction>> DelelteProductFromOrderAsync(Guid orderId, Guid orderItemId, CancellationToken cancellationToken)
    {
        var foundOrder = await dbContext.BattleGroups
            .Where(model => model.Id == orderId)
            .Include(e => e.BattleGroupUnits)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (foundOrder is null)
            return PersistenceResult<SuccsefullTransaction>.Failure(NotFound.WithMessage($"Unable to delete OrderItem with ID: {orderItemId}, as no Order with id: {orderId} was found."));

        var foundOrderItem = foundOrder.BattleGroupUnits.SingleOrDefault(model => model.Id == orderItemId);

        if (foundOrderItem is null)
            return PersistenceResult<SuccsefullTransaction>.Failure(NotFound.WithMessage($"Unable to delete OrderItem with ID: {orderItemId} from Order with id: {orderId}."));
        
        foundOrder.BattleGroupUnits.Remove(foundOrderItem);
        await dbContext.SaveChangesAsync(cancellationToken);

        return PersistenceResult<SuccsefullTransaction>
            .Success(TypedResult<SuccsefullTransaction>
                .Of(new SuccsefullTransaction($"Deleted OrderItem with id: {orderItemId} from Order with id: {orderId}")));
    }

    public async Task<PersistenceResult<SuccsefullTransaction>> DeleteToppingFromProductsOrderAsync(Guid orderId, Guid orderItemId, Guid orderToppingId,
        CancellationToken cancellationToken)
    {
        var foundOrder = await dbContext.BattleGroups
            .Where(model => model.Id == orderId)
            .Include(e => e.BattleGroupUnits)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (foundOrder is null)
            return PersistenceResult<SuccsefullTransaction>.Failure(NotFound.WithMessage($"Unable to delete ToppingItem with ID: {orderToppingId}, as no Order with id: {orderId} was found."));

        var foundOrderItem = foundOrder.BattleGroupUnits.SingleOrDefault(model => model.Id == orderItemId);

        if (foundOrderItem is null)
            return PersistenceResult<SuccsefullTransaction>.Failure(NotFound.WithMessage($"Unable to delete ToppingItem with ID: {orderToppingId} from OrderItem with id: {orderItemId} - OrderItem was not found."));
        
        var foundToppingItem = await dbContext.BattleGroupUnitEquipments.Where(model => model.Id == orderToppingId).SingleOrDefaultAsync(cancellationToken);
        
        if (foundToppingItem is null)
            return PersistenceResult<SuccsefullTransaction>.Failure(NotFound.WithMessage($"Unable to delete ToppingItem with ID: {orderToppingId} from OrderItem with id: {orderItemId} - OrderToppingItem not found."));
        
        foundOrderItem.BattleGroupUnitEquipments.Remove(foundToppingItem);
        
        foundOrder.BattleGroupUnits.Remove(foundOrderItem);
        await dbContext.SaveChangesAsync(cancellationToken);

        return PersistenceResult<SuccsefullTransaction>
            .Success(TypedResult<SuccsefullTransaction>
                .Of(new SuccsefullTransaction($"Deleted OrderToppingItem with id: {orderToppingId} from Order with id: {orderId}")));
    }
}
