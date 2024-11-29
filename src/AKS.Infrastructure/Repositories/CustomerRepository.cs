
using AKS.Application.Repositories;
using AKS.Domain.Entities;
using AKS.Domain.Results;
using AKS.Domain.Results.Errors;
using AKS.Domain.Results.Status;
using AKS.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

using Guid = System.Guid;

namespace AKS.Infrastructure.Repositories;

public class CustomerRepository(PrimaryDbContext primaryDbContext) : ICustomerRepository
{
    public async Task<Customer> CreateCustomerAsync(Customer customer, CancellationToken cancellationToken)
    {
        primaryDbContext.Customers.Add(customer);
        await primaryDbContext
            .SaveChangesAsync(cancellationToken);
        return customer;
    }

    public async Task<PersistenceResult<SuccsefullTransaction>> DeleteCustomerAsync(Guid customerId, CancellationToken cancellationToken)
    {
        var affected = await primaryDbContext.Customers
                .Where(model => model.Id == customerId)
                .ExecuteDeleteAsync(cancellationToken);
        return affected == 1 ? 
                PersistenceResult<SuccsefullTransaction>
                    .Success(TypedResult<SuccsefullTransaction>
                        .Of(new SuccsefullTransaction($"Deleted Customer with id: {customerId}"))) : 
                PersistenceResult<SuccsefullTransaction>.Failure(NotFound.Empty());
    }

    public async Task<PersistenceResult<Customer>> GetCustomerAsync(Guid customerId, CancellationToken cancellationToken)
    {
        return await primaryDbContext.Customers.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == customerId, cancellationToken: cancellationToken)
                is Customer model
                    ? PersistenceResult<Customer>.Success(TypedResult<Customer>.Of(model))
                    : PersistenceResult<Customer>.Failure(NotFound.Empty());
    }

    public async Task<List<Customer>> GetCustomersAsync(CancellationToken cancellationToken)
    {
        return await primaryDbContext.Customers.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<PersistenceResult<Customer>> UpdateCustomerAsync(Guid customerId, Customer customer, CancellationToken cancellationToken)
    {
        var affected = await primaryDbContext.Customers
                .Where(model => model.Id == customerId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, customer.Id)
                    .SetProperty(m => m.FirstName, customer.FirstName)
                    .SetProperty(m => m.LastName, customer.LastName)
                    .SetProperty(m => m.Address, customer.Address)
                    , cancellationToken: cancellationToken);
        return affected == 1 ? 
                PersistenceResult<Customer>.Success(TypedResult<Customer>.Of(customer)) : 
                PersistenceResult<Customer>.Failure(NotFound.Empty());
    }
}
