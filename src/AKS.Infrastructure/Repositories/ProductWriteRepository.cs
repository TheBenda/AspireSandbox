using AKS.Application.Repositories;
using AKS.Domain.Entities;
using AKS.Domain.Results;
using AKS.Domain.Results.Errors;
using AKS.Domain.Results.Status;
using AKS.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace AKS.Infrastructure.Repositories;

public class ProductWriteRepository(PrimaryDbContext dbContext) : IProductWriteRepository
{
    public async Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken)
    {
        dbContext.Products.Add(product);
        await dbContext
            .SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<PersistenceResult<SuccsefullTransaction>> DeleteProductAsync(Guid productId, CancellationToken cancellationToken)
    {
        var affected = await dbContext.Products
            .Where(model => model.Id == productId)
            .ExecuteDeleteAsync(cancellationToken);
        return affected == 1 ? 
            PersistenceResult<SuccsefullTransaction>
                .Success(TypedResult<SuccsefullTransaction>
                    .Of(new SuccsefullTransaction($"Deleted Customer with id: {productId}"))) : 
            PersistenceResult<SuccsefullTransaction>.Failure(NotFound.Empty());
    }

    public async Task<PersistenceResult<Product>> CreateToppingToProductAsync(Guid productId, Topping topping, CancellationToken cancellationToken)
    {
        var foundProduct = await dbContext.Products
            .Where(model => model.Id == productId)
            .Include(model => model.Toppings)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (foundProduct is null)
            return PersistenceResult<Product>.Failure(NotFound.WithMessage($"Unable to add new Topping to Product with id: {productId} - Product was not found."));
        
        foundProduct.Toppings.Add(topping);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return PersistenceResult<Product>.Success(TypedResult<Product>.Of(foundProduct));
    }

    public async Task<PersistenceResult<SuccsefullTransaction>> DeleteToppingAsync(Guid productId, Guid toppingId, CancellationToken cancellationToken)
    {
        var foundProduct = await dbContext.Products
            .Where(model => model.Id == productId)
            .Include(p => p.Toppings)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (foundProduct is null)
            return PersistenceResult<SuccsefullTransaction>.Failure(NotFound.WithMessage($"Unable to delete Topping with ID: {toppingId}, as no Product with id: {productId} was found."));

        var foundTopping = foundProduct.Toppings.SingleOrDefault(model => model.Id == toppingId);

        if (foundTopping is null)
            return PersistenceResult<SuccsefullTransaction>.Failure(NotFound.WithMessage($"Unable to delete Topping with ID: {toppingId} from Product with id: {productId} - This Product don't contain this topping."));
        
        foundProduct.Toppings.Remove(foundTopping);
        await dbContext.SaveChangesAsync(cancellationToken);

        return PersistenceResult<SuccsefullTransaction>
            .Success(TypedResult<SuccsefullTransaction>
                .Of(new SuccsefullTransaction($"Deleted Topping with id: {toppingId} from Product with id: {productId}")));
    }
}