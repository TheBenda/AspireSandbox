using AKS.Application.Repositories;
using AKS.Domain.Entities;
using AKS.Domain.Results;
using AKS.Domain.Results.Errors;
using AKS.Domain.Results.Status;
using AKS.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

using Guid = System.Guid;

namespace AKS.Infrastructure.Repositories;

public class UnitWriteRepository(PrimaryDbContext dbContext) : IUnitWriteRepository
{
    public async Task<Unit> CreateProductAsync(Unit unit, CancellationToken cancellationToken)
    {
        dbContext.Units.Add(unit);
        await dbContext
            .SaveChangesAsync(cancellationToken);
        return unit;
    }

    public async Task<PersistenceResult<SuccsefullTransaction>> DeleteProductAsync(Guid productId, CancellationToken cancellationToken)
    {
        var affected = await dbContext.Units
            .Where(model => model.Id == productId)
            .ExecuteDeleteAsync(cancellationToken);
        return affected == 1 ? 
            PersistenceResult<SuccsefullTransaction>
                .Success(TypedResult<SuccsefullTransaction>
                    .Of(new SuccsefullTransaction($"Deleted Customer with id: {productId}"))) : 
            PersistenceResult<SuccsefullTransaction>.Failure(NotFound.Empty());
    }

    public async Task<PersistenceResult<Unit>> CreateToppingToProductAsync(Guid productId, Equipment equipment, CancellationToken cancellationToken)
    {
        var foundProduct = await dbContext.Units
            .Where(model => model.Id == productId)
            .Include(model => model.Equipments)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (foundProduct is null)
            return PersistenceResult<Unit>.Failure(NotFound.WithMessage($"Unable to add new Topping to Product with id: {productId} - Product was not found."));
        
        foundProduct.Equipments.Add(equipment);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return PersistenceResult<Unit>.Success(TypedResult<Unit>.Of(foundProduct));
    }

    public async Task<PersistenceResult<SuccsefullTransaction>> DeleteToppingAsync(Guid productId, Guid toppingId, CancellationToken cancellationToken)
    {
        var foundProduct = await dbContext.Units
            .Where(model => model.Id == productId)
            .Include(p => p.Equipments)
            .SingleOrDefaultAsync(cancellationToken);
        
        if (foundProduct is null)
            return PersistenceResult<SuccsefullTransaction>.Failure(NotFound.WithMessage($"Unable to delete Topping with ID: {toppingId}, as no Product with id: {productId} was found."));

        var foundTopping = foundProduct.Equipments.SingleOrDefault(model => model.Id == toppingId);

        if (foundTopping is null)
            return PersistenceResult<SuccsefullTransaction>.Failure(NotFound.WithMessage($"Unable to delete Topping with ID: {toppingId} from Product with id: {productId} - This Product don't contain this topping."));
        
        foundProduct.Equipments.Remove(foundTopping);
        await dbContext.SaveChangesAsync(cancellationToken);

        return PersistenceResult<SuccsefullTransaction>
            .Success(TypedResult<SuccsefullTransaction>
                .Of(new SuccsefullTransaction($"Deleted Topping with id: {toppingId} from Product with id: {productId}")));
    }
}