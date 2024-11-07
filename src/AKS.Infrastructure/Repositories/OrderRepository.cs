using AKS.Application.Repositories;
using AKS.Domain.Entities;
using AKS.Infrastructure.Data;

namespace AKS.Infrastructure.Repositories;

public class OrderRepository(PrimaryDbContext dbContext) : IOrderRepository
{
    public async Task<Order> CreateAsync(Order order, CancellationToken cancellationToken)
    {
        dbContext.Orders.Add(order);
        await dbContext
            .SaveChangesAsync(cancellationToken);
        return order;
    }
}
