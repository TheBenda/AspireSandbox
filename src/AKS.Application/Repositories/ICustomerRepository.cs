using AKS.Domain.Entities;
using AKS.Domain.Results;
using AKS.Domain.Results.Status;

namespace AKS.Application.Repositories;

public interface ICustomerRepository
{
    public Task<List<Customer>> GetCustomersAsync(CancellationToken cancellationToken);
    public Task<PersistenceResult<Customer>> GetCustomerAsync(Guid customerId, CancellationToken cancellationToken);
    public Task<PersistenceResult<Customer>> UpdateCustomerAsync(Guid customerId, Customer customer, CancellationToken cancellationToken);
    public Task<Customer> CreateCustomerAsync(Customer customer, CancellationToken cancellationToken);
    public Task<PersistenceResult<SuccsefullTransaction>> DeleteCustomerAsync(Guid customerId, CancellationToken cancellationToken);
}
