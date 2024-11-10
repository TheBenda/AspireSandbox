using AKS.Application.Mapping;
using AKS.Application.Mapping.Customers;
using AKS.Application.Repositories;

namespace AKS.Application.UseCases.Customers.GetById;

public static class FindCustomerHandler
{
    public static async Task<CustomerFound> Handle(FindCustomer request, ICustomerRepository customerRepository, CancellationToken cancelationToken)
    {
        var foundCustomer = await customerRepository.GetCustomerAsync(request.CustomerId, cancelationToken).ConfigureAwait(false);
        return CustomerFound.New(foundCustomer.ToDto(CustomersExtensions.MapToCustomerDto));
    }
}