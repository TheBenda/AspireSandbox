using AKS.Application.Repositories;
using AKS.Domain.Entities;
using AKS.Domain.Results;
using AKS.Domain.Results.Erros;
using AKS.Domain.Results.Status;
using AKS.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace AKS.Infrastructure.Repositories;

public class ProductRepository(PrimaryDbContext dbContext) : IProductRepository
{
    public async Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken)
    {
        dbContext.Products.Add(product);
        await dbContext
            .SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<PersistenceResult<Product>> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        => await dbContext.Products.AsNoTracking()
                .Include(p => p.Toppings)
                .FirstOrDefaultAsync(model => model.Id == id, cancellationToken: cancellationToken)
            is Product model
            ? PersistenceResult<Product>.Success(TypedResult<Product>.Of(model))
            : PersistenceResult<Product>.Failure(NotFound.Empty());

    public async Task<PersistenceResult<SuccsefullTransaction>> DeleteProductAsync(Guid productId, CancellationToken cancellationToken)
    {
        var affected = await dbContext.Customers
            .Where(model => model.Id == productId)
            .ExecuteDeleteAsync(cancellationToken);
        return affected == 1 ? 
            PersistenceResult<SuccsefullTransaction>
                .Success(TypedResult<SuccsefullTransaction>
                    .Of(new SuccsefullTransaction($"Deleted Customer with id: {productId}"))) : 
            PersistenceResult<SuccsefullTransaction>.Failure(NotFound.Empty());
    }

    public async Task<List<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
        => await dbContext.Products.ToListAsync(cancellationToken);
}
