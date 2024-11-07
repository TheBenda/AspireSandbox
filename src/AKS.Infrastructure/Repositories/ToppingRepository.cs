using AKS.Application.Repositories;
using AKS.Domain.Entities;
using AKS.Domain.Results;
using AKS.Domain.Results.Erros;
using AKS.Domain.Results.Status;
using AKS.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace AKS.Infrastructure.Repositories;

public class ToppingRepository(PrimaryDbContext dbContext) : IToppingRepository
{
    public async Task<Topping> CreateToppingAsync(Topping topping, CancellationToken cancellationToken)
    {
        dbContext.Toppings.Add(topping);
        await dbContext
            .SaveChangesAsync(cancellationToken);
        return topping;
    }

    public async Task<PersistenceResult<SuccsefullTransaction>> DeleteProductAsync(Guid toppingId, CancellationToken cancellationToken)
    {
        var affected = await dbContext.Customers
            .Where(model => model.Id == toppingId)
            .ExecuteDeleteAsync(cancellationToken);
        return affected == 1 ? 
            PersistenceResult<SuccsefullTransaction>
                .Success(TypedResult<SuccsefullTransaction>
                    .Of(new SuccsefullTransaction($"Deleted Customer with id: {toppingId}"))) : 
            PersistenceResult<SuccsefullTransaction>.Failure(NotFound.Empty());
    }
}