using AKS.Application.UseCases.Equipments.Create;
using AKS.Application.UseCases.Units.Create;

using Bogus;

namespace AKS.IntegrationTests.Fixtures.Units;

public static class UnitsFixtures
{
    private static readonly Faker Faker = new();

    public static CreateUnit CreateUnit()
        => new CreateUnit(
            Faker.Commerce.ProductName(), 
            Faker.Lorem.Sentence(),
            Faker.Random.Int(0, 100),
            Faker.Random.Int(0, 100),
            Faker.Random.Int(0, 100),
            Faker.Random.Int(0, 100),
            Faker.Random.Decimal(0, 100),
            Faker.Random.Int(1, 100),
            Faker.Random.Decimal(1, 15));

    public static AddEquipmentToUnit AddEquipment(Guid unitId)
        => new AddEquipmentToUnit(
            unitId, Faker.Commerce.ProductName(),
            Faker.Lorem.Sentence(),
            Faker.Random.Int(0, 100),
            Faker.Random.Decimal(1, 10));
}