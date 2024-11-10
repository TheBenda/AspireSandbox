using AKS.Application.UseCases.Customers.GetById;
using AKS.Application.UseCases.Customers.Transport;
using AKS.Domain.Entities;

namespace AKS.Application.UseCases.Customers.GetAll;

public record CustomersFound(List<CustomerDto> Customers)
{
    public static CustomersFound New(List<CustomerDto> customers) => new (customers);
}