using AKS.Application.Repositories;
using AKS.Domain.Entities;
using AKS.Domain.Results;
using AKS.Domain.Results.Errors;
using AKS.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace AKS.Infrastructure.Repositories;

public class OrderReadRepository(PrimaryDbContext dbContext) : IOrderReadRepository
{
    public async Task<PersistenceResult<Order>> GetAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var foundOrder = await dbContext.Orders
            .AsNoTracking()
            .Where(o => o.Id == orderId)
            .Include(m => m.OrderItems)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (foundOrder is null)
            return PersistenceResult<Order>.Failure(NotFound.WithMessage($"Unable to find Order with id: {orderId}."));

        var orderItemsIds = foundOrder.OrderItems.Select(orderItem => orderItem.Id).ToList();

        var orderToppingItems = await dbContext.OrderToppingItems
            .AsNoTracking()
            .Where(m => orderItemsIds.Contains(m.OrderItemId))
            .ToDictionaryAsync(key => key.OrderItemId, cancellationToken);
        
        foreach (var foundOrderOrderItem in foundOrder.OrderItems)
        {
            foundOrderOrderItem.OrderToppingItems.Add(orderToppingItems[foundOrderOrderItem.Id]);
        }
        
        return PersistenceResult<Order>.Success(TypedResult<Order>.Of(foundOrder));
    }

    public Task<List<Order>> GetAllAsync(CancellationToken cancellationToken)
        => dbContext.Orders.AsNoTracking().ToListAsync(cancellationToken);
}