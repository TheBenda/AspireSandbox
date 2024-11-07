using AKS.Application.UseCases.Customers.GetById;
using AKS.Domain.Entities;

namespace AKS.Application.UseCases.Customers.GetAll;

public record CustomersFound(List<CustomerFound> Customers)
{
    public static CustomersFound New(List<CustomerFound> customers) => new (customers);
}