using AKS.Application.Repositories;
using AKS.Domain.Results;
using AKS.Domain.Results.Status;

namespace AKS.Application.UseCases.Customers.Delete;

public static class DeleteCustomerHandler
{
    public static async Task<CustomerDeleted> Handle(DeleteCustomer command, ICustomerRepository customerRepository, CancellationToken cancellationToken) 
        => CustomerDeleted.New(await customerRepository.DeleteCustomerAsync(command.CustomerId, cancellationToken).ConfigureAwait(false));
}