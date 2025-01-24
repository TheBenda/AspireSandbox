using AKS.Application.Repositories;
using AKS.Domain.Entities;
using AKS.Domain.Results;
using AKS.Domain.Results.Errors;
using AKS.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

using Guid = System.Guid;

namespace AKS.Infrastructure.Repositories;

public class UnitReadRepository(PrimaryDbContext dbContext) : IUnitReadRepository
{
    public async Task<PersistenceResult<Unit>> GetProductByIdAsync(Guid id, CancellationToken cancellationToken) 
        => await dbContext.Units
                .AsNoTracking()
                .Include(p => p.Equipments)
                .FirstOrDefaultAsync(model => model.Id == id, cancellationToken: cancellationToken)
            is Unit model
            ? PersistenceResult<Unit>.Success(TypedResult<Unit>.Of(model))
            : PersistenceResult<Unit>.Failure(NotFound.Empty());
    

    public async Task<List<Unit>> GetAllProductsAsync(CancellationToken cancellationToken)
        => await dbContext.Units.ToListAsync(cancellationToken);
}