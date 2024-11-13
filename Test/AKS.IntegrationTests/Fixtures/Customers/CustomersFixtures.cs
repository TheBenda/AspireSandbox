using AKS.Application.UseCases.Customers.Create;

using Bogus;

namespace AKS.IntegrationTests.Fixtures.Customers;

public static class CustomersFixtures
{
    private static Faker _faker = new();

    public static CreateCustomer CreateCustomer()
        => new CreateCustomer(
            _faker.Name.FirstName(),
            _faker.Name.LastName(),
            _faker.Address.StreetAddress(),
            _faker.Address.City(),
            _faker.Address.State(),
            _faker.Address.Country(),
            _faker.Address.ZipCode(),
            _faker.Address.Latitude(),
            _faker.Address.Longitude()
            );
}