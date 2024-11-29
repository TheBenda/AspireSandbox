using AKS.Domain.Entities;
using AKS.Domain.Results;
using AKS.Domain.Results.Status;

namespace AKS.Application.Repositories;

public interface IOrderWriteRepository
{
    Task<Order> CreateAsync(Order order, CancellationToken cancellationToken);
    Task<PersistenceResult<Order>> AddProductToOrderAsync(Guid orderId, Guid productId, CancellationToken cancellationToken);
    Task<PersistenceResult<Order>> AddToppingToProductsOrderAsync(Guid orderId, Guid orderItemId, OrderToppingItem orderToppingItem, CancellationToken cancellationToken);
    Task<PersistenceResult<SuccsefullTransaction>> DeleteAsync(Guid orderId, CancellationToken cancellationToken);
    Task<PersistenceResult<SuccsefullTransaction>> DelelteProductFromOrderAsync(Guid orderId, Guid orderItemId, CancellationToken cancellationToken);
    Task<PersistenceResult<SuccsefullTransaction>> DeleteToppingFromProductsOrderAsync(Guid orderId, Guid orderItemId, Guid orderToppingId, CancellationToken cancellationToken);
}
