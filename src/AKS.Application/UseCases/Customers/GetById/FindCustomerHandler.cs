using AKS.Application.Repositories;

namespace AKS.Application.UseCases.Customers.GetById;

public static class FindCustomerHandler
{
    public static async Task<CustomerFound> Handle(FindCustomer request, ICustomerRepository customerRepository, CancellationToken cancelationToken)
    {
        var foundCustomer = await customerRepository.GetCustomerAsync(request.CustomerId, cancelationToken).ConfigureAwait(false);
        throw new NotImplementedException("TODO: what to do with mapping");
    }
}