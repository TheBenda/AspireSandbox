using AKS.Application.UseCases.Customers.GetById;
using AKS.Domain.Entities;

namespace AKS.Application.Mapping.Customers;

internal static class CustomersExtensions
{
    internal static CustomerFound MapToCustomerFound(this Customer customer)
        => new CustomerFound(customer.Id,
            customer.FirstName,
            customer.LastName,
            customer.Address?.Street,
            customer.Address?.City,
            customer.Address?.State,
            customer.Address?.Country,
            customer.Address?.ZipCode,
            customer.Address.Latitude,
            customer.Address.Longitude);

    internal static List<CustomerFound> MapToCustomerList(this IEnumerable<Customer> customers)
        => customers.Select(c => c.MapToCustomerFound()).ToList();
}