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

    public async Task<PersistenceResult<BattleGroup>> AddProductToOrderAsync(Guid orderId, Guid productId, CancellationToken cancellationToken)
    {
        var foundOrder = await dbContext.BattleGroups
            .Where(model => model.Id == orderId)
            .Include(e => e.BattleGroupUnits)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (foundOrder is null)
            return PersistenceResult<BattleGroup>.Failure(NotFound.WithMessage($"Unable to add OrderItem, as no Order with id: {orderId} was found."));
        
        var foundProduct = await dbContext.Units
            .AsNoTracking()
            .Where(model => model.Id == productId)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (foundProduct is null)
            return PersistenceResult<BattleGroup>.Failure(NotFound.WithMessage($"Unable to add OrderItem, as no Product with id: {productId} was found."));
        
        // var orderItem = new OrderItem
        // {
        //     Name = foundProduct.Name,
        //     Price = foundProduct.Price,
        //     Quantity = 1,
        //     ProductId = foundProduct.Id
        // };
        
        // foundOrder.OrderItems.Add(orderItem);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return PersistenceResult<BattleGroup>.Success(TypedResult<BattleGroup>.Of(foundOrder));
    }

    public async Task<PersistenceResult<BattleGroup>> AddToppingToProductsOrderAsync(Guid orderId, Guid orderItemId, BattleGroupUnitEquipment battleGroupUnitEquipment,
        CancellationToken cancellationToken)
    {
        var foundOrder = await dbContext.BattleGroups
            .Where(model => model.Id == orderId)
            .Include(e => e.BattleGroupUnits)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (foundOrder is null)
            return PersistenceResult<BattleGroup>.Failure(NotFound.WithMessage($"Unable to add OrderToppingItem, as no Order with id: {orderId} was found."));
        
        var foundOrderItem = await dbContext.BattleGroupUnits
            .Where(model => model.Id == orderItemId)
            .Include(e => e.BattleGroupUnitEquipments)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (foundOrderItem is null)
            return PersistenceResult<BattleGroup>.Failure(NotFound.WithMessage($"Unable to add OrderToppingItem, as no OrderItem with id: {orderItemId} was found."));
        
        foundOrderItem.BattleGroupUnitEquipments.Add(battleGroupUnitEquipment);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        // TODO: proof that added OrderToppingItem is projected into Order
        return PersistenceResult<BattleGroup>.Success(TypedResult<BattleGroup>.Of(foundOrder));
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
