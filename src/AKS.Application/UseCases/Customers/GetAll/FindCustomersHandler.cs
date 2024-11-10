using AKS.Application.Mapping.Customers;
using AKS.Application.Repositories;

namespace AKS.Application.UseCases.Customers.GetAll;

public static class FindCustomersHandler
{
    public static async Task<CustomersFound> Handle(FindCustomers request, ICustomerRepository repository, CancellationToken cancellationToken)
    {
        var customers = await repository.GetCustomersAsync(cancellationToken).ConfigureAwait(false);
        return CustomersFound.New(customers.MapToCustomerDtos());
    }
}