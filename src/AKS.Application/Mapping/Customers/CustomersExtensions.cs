using AKS.Application.UseCases.Customers.GetById;
using AKS.Application.UseCases.Customers.Transport;
using AKS.Domain.Entities;

namespace AKS.Application.Mapping.Customers;

internal static class CustomersExtensions
{
    internal static CustomerDto MapToCustomerDto(this Customer customer)
        => new CustomerDto(customer.Id,
            customer.FirstName,
            customer.LastName,
            customer.Address?.Street,
            customer.Address?.City,
            customer.Address?.State,
            customer.Address?.Country,
            customer.Address?.ZipCode,
            customer.Address.Latitude,
            customer.Address.Longitude);
    
    // internal static CustomerDto MapToCustomerDto(Customer customer)
    //     => new CustomerDto(customer.Id,
    //         customer.FirstName,
    //         customer.LastName,
    //         customer.Address?.Street,
    //         customer.Address?.City,
    //         customer.Address?.State,
    //         customer.Address?.Country,
    //         customer.Address?.ZipCode,
    //         customer.Address.Latitude,
    //         customer.Address.Longitude);

    internal static List<CustomerDto> MapToCustomerDtos(this IEnumerable<Customer> customers)
        => customers.Select(c => c.MapToCustomerDto()).ToList();
}