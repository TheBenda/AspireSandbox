using System.Security.Cryptography;

using AKS.Application.UseCases.Products.Create;
using AKS.Application.UseCases.Toppings.Create;

using Bogus;

namespace AKS.IntegrationTests.Fixtures.Products;

public static class ProductsFixtures
{
    private static readonly Faker _faker = new();

    public static CreateProduct CreateProduct()
        => new CreateProduct(_faker.Commerce.ProductName(), _faker.Random.Decimal());

    public static AddToppingToProduct AddTopping(Guid productId)
        => new AddToppingToProduct(productId, _faker.Commerce.ProductName(), _faker.Random.Decimal());
}