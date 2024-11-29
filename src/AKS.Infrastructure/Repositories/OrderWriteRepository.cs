using AKS.Application.Repositories;
using AKS.Domain.Entities;
using AKS.Domain.Results;
using AKS.Domain.Results.Errors;
using AKS.Domain.Results.Status;
using AKS.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace AKS.Infrastructure.Repositories;

public class OrderWriteRepository(PrimaryDbContext dbContext) : IOrderWriteRepository
{
    public async Task<Order> CreateAsync(Order order, CancellationToken cancellationToken)
    {
        dbContext.Orders.Add(order);
        await dbContext
            .SaveChangesAsync(cancellationToken);
        return order;
    }

    public async Task<PersistenceResult<Order>> AddProductToOrderAsync(Guid orderId, Guid productId, CancellationToken cancellationToken)
    {
        var foundOrder = await dbContext.Orders
            .Where(model => model.Id == orderId)
            .Include(e => e.OrderItems)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (foundOrder is null)
            return PersistenceResult<Order>.Failure(NotFound.WithMessage($"Unable to add OrderItem, as no Order with id: {orderId} was found."));
        
        var foundProduct = await dbContext.Units
            .AsNoTracking()
            .Where(model => model.Id == productId)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (foundProduct is null)
            return PersistenceResult<Order>.Failure(NotFound.WithMessage($"Unable to add OrderItem, as no Product with id: {productId} was found."));
        
        // var orderItem = new OrderItem
        // {
        //     Name = foundProduct.Name,
        //     Price = foundProduct.Price,
        //     Quantity = 1,
        //     ProductId = foundProduct.Id
        // };
        
        // foundOrder.OrderItems.Add(orderItem);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return PersistenceResult<Order>.Success(TypedResult<Order>.Of(foundOrder));
    }

    public async Task<PersistenceResult<Order>> AddToppingToProductsOrderAsync(Guid orderId, Guid orderItemId, OrderToppingItem orderToppingItem,
        CancellationToken cancellationToken)
    {
        var foundOrder = await dbContext.Orders
            .Where(model => model.Id == orderId)
            .Include(e => e.OrderItems)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (foundOrder is null)
            return PersistenceResult<Order>.Failure(NotFound.WithMessage($"Unable to add OrderToppingItem, as no Order with id: {orderId} was found."));
        
        var foundOrderItem = await dbContext.OrderItems
            .Where(model => model.Id == orderItemId)
            .Include(e => e.OrderToppingItems)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (foundOrderItem is null)
            return PersistenceResult<Order>.Failure(NotFound.WithMessage($"Unable to add OrderToppingItem, as no OrderItem with id: {orderItemId} was found."));
        
        foundOrderItem.OrderToppingItems.Add(orderToppingItem);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        // TODO: proof that added OrderToppingItem is projected into Order
        return PersistenceResult<Order>.Success(TypedResult<Order>.Of(foundOrder));
    }

    public async Task<PersistenceResult<SuccsefullTransaction>> DeleteAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var affected = await dbContext.Orders
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
        var foundOrder = await dbContext.Orders
            .Where(model => model.Id == orderId)
            .Include(e => e.OrderItems)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (foundOrder is null)
            return PersistenceResult<SuccsefullTransaction>.Failure(NotFound.WithMessage($"Unable to delete OrderItem with ID: {orderItemId}, as no Order with id: {orderId} was found."));

        var foundOrderItem = foundOrder.OrderItems.SingleOrDefault(model => model.Id == orderItemId);

        if (foundOrderItem is null)
            return PersistenceResult<SuccsefullTransaction>.Failure(NotFound.WithMessage($"Unable to delete OrderItem with ID: {orderItemId} from Order with id: {orderId}."));
        
        foundOrder.OrderItems.Remove(foundOrderItem);
        await dbContext.SaveChangesAsync(cancellationToken);

        return PersistenceResult<SuccsefullTransaction>
            .Success(TypedResult<SuccsefullTransaction>
                .Of(new SuccsefullTransaction($"Deleted OrderItem with id: {orderItemId} from Order with id: {orderId}")));
    }

    public async Task<PersistenceResult<SuccsefullTransaction>> DeleteToppingFromProductsOrderAsync(Guid orderId, Guid orderItemId, Guid orderToppingId,
        CancellationToken cancellationToken)
    {
        var foundOrder = await dbContext.Orders
            .Where(model => model.Id == orderId)
            .Include(e => e.OrderItems)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (foundOrder is null)
            return PersistenceResult<SuccsefullTransaction>.Failure(NotFound.WithMessage($"Unable to delete ToppingItem with ID: {orderToppingId}, as no Order with id: {orderId} was found."));

        var foundOrderItem = foundOrder.OrderItems.SingleOrDefault(model => model.Id == orderItemId);

        if (foundOrderItem is null)
            return PersistenceResult<SuccsefullTransaction>.Failure(NotFound.WithMessage($"Unable to delete ToppingItem with ID: {orderToppingId} from OrderItem with id: {orderItemId} - OrderItem was not found."));
        
        var foundToppingItem = await dbContext.OrderToppingItems.Where(model => model.Id == orderToppingId).SingleOrDefaultAsync(cancellationToken);
        
        if (foundToppingItem is null)
            return PersistenceResult<SuccsefullTransaction>.Failure(NotFound.WithMessage($"Unable to delete ToppingItem with ID: {orderToppingId} from OrderItem with id: {orderItemId} - OrderToppingItem not found."));
        
        foundOrderItem.OrderToppingItems.Remove(foundToppingItem);
        
        foundOrder.OrderItems.Remove(foundOrderItem);
        await dbContext.SaveChangesAsync(cancellationToken);

        return PersistenceResult<SuccsefullTransaction>
            .Success(TypedResult<SuccsefullTransaction>
                .Of(new SuccsefullTransaction($"Deleted OrderToppingItem with id: {orderToppingId} from Order with id: {orderId}")));
    }
}
