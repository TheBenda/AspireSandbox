using AKS.Application.Repositories;
using AKS.Domain.Entities;
using AKS.Domain.Values;

namespace AKS.Application.UseCases.Customers.Create;

public static class CreateCustomerHandler
{
    public static async Task<CustomerCreated> Handle(CreateCustomer request, ICustomerRepository repository, CancellationToken cancellation)
    {
        var customer = new Customer
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Address = new Address
            {
              Street = request.Street,
              City = request.City,
              Country = request.Country,
              State = request.State,
              ZipCode = request.ZipCode,
              Latitude = request.Latitude,
              Longitude = request.Longitude
            }
        };
        var createdCustomer = await repository.CreateCustomerAsync(customer, cancellation);
        
        return new CustomerCreated(createdCustomer.Id, 
            customer.FirstName,
            customer.LastName,
            customer.Address?.Street,
            customer.Address?.City,
            customer.Address?.State,
            customer.Address?.Country,
            customer.Address?.ZipCode,
            customer.Address.Latitude,
            customer.Address.Longitude);
    }
}