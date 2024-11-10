using AKS.Application.UseCases.Customers.Transport;
using AKS.Domain.Results;

namespace AKS.Application.UseCases.Customers.GetById;

public record CustomerFound(PersistenceResult<CustomerDto> Customer)
{
    public static CustomerFound New(PersistenceResult<CustomerDto> customer) 
        => new CustomerFound(customer);
}