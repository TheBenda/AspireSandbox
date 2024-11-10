using AKS.Application.Repositories;
using AKS.Domain.Entities;
using AKS.Domain.Results;
using AKS.Domain.Results.Errors;
using AKS.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace AKS.Infrastructure.Repositories;

public class ProductReadRepository(PrimaryDbContext dbContext) : IProductReadRepository
{
    public async Task<PersistenceResult<Product>> GetProductByIdAsync(Guid id, CancellationToken cancellationToken) 
        => await dbContext.Products.AsNoTracking()
                .Include(p => p.Toppings)
                .FirstOrDefaultAsync(model => model.Id == id, cancellationToken: cancellationToken)
            is Product model
            ? PersistenceResult<Product>.Success(TypedResult<Product>.Of(model))
            : PersistenceResult<Product>.Failure(NotFound.Empty());
    

    public async Task<List<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
        => await dbContext.Products.ToListAsync(cancellationToken);
}