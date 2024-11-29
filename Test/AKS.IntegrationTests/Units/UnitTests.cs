using AKS.Application.UseCases.Equipments.Create;
using AKS.Application.UseCases.Units.Create;
using AKS.Application.UseCases.Units.Delete;
using AKS.Application.UseCases.Units.GetAll;
using AKS.Application.UseCases.Units.GetById;
using AKS.Domain.Results;
using AKS.IntegrationTests.Fixtures.Units;

namespace AKS.IntegrationTests.Units;

[ClassDataSource<BaseIntegrationTest>(Shared = SharedType.PerAssembly)]
public class UnitTests(BaseIntegrationTest baseTest)
{
    [Test]
    public async Task CreateUnit()
    {
        var command = UnitsFixtures.CreateUnit();
        var createdUnit = await baseTest.MessageBus.InvokeAsync<UnitCreated>(command);

        await Assert.That(createdUnit.Id).IsNotNull();
        await Assert.That(createdUnit.Name).IsEqualTo(command.Name);
        await Assert.That(createdUnit.Rule).IsEqualTo(command.Rule);
        await Assert.That(createdUnit.Attack).IsEqualTo(command.Attack);
        await Assert.That(createdUnit.Defense).IsEqualTo(command.Defense);
        await Assert.That(createdUnit.Movement).IsEqualTo(command.Movement);
        await Assert.That(createdUnit.Points).IsEqualTo(command.Points);
    }
    
    [Test]
    public async Task AddEquipmentToUnit()
    {
        var createUnitCommand = UnitsFixtures.CreateUnit();
        var createdUnit = await baseTest.MessageBus.InvokeAsync<UnitCreated>(createUnitCommand);
        
        var addEquipmentCommand = UnitsFixtures.AddEquipment(createdUnit.Id);
        
        var equipmentAddedResult = await baseTest.MessageBus.InvokeAsync<AddedEquipmentToUnit>(addEquipmentCommand);
        
        await Assert.That(equipmentAddedResult.Result.Type).IsEqualTo(ResultType.Result);
        
        var addedEquipment = equipmentAddedResult.Result.Result!.Value;

        await Assert.That(addedEquipment.Equipments.Count).IsEqualTo(1);
        await Assert.That(addedEquipment.Equipments[0].Id).IsNotNull();
        await Assert.That(addedEquipment.Equipments[0].Name).IsEqualTo(addEquipmentCommand.Name);
        await Assert.That(addedEquipment.Equipments[0].Rule).IsEqualTo(addEquipmentCommand.Rule);
        await Assert.That(addedEquipment.Equipments[0].Attack).IsEqualTo(addEquipmentCommand.Attack);
        await Assert.That(addedEquipment.Equipments[0].Points).IsEqualTo(addEquipmentCommand.Points);
    }

    [Test]
    public async Task AddEquipmentToNotExistingUnit()
    {
        var addToppingCommand = UnitsFixtures.AddEquipment(Guid.NewGuid());
        
        var addedTopicResult = await baseTest.MessageBus.InvokeAsync<AddedEquipmentToUnit>(addToppingCommand);
        
        await Assert.That(addedTopicResult.Result.Type).IsEqualTo(ResultType.Error);
    }

    [Test]
    public async Task DeleteUnit()
    {
        var createProductCommand = UnitsFixtures.CreateUnit();
        var createdProduct = await baseTest.MessageBus.InvokeAsync<UnitCreated>(createProductCommand);
        
        var deleteProductResult = await baseTest.MessageBus
            .InvokeAsync<UnitDeleted>(new DeleteUnit(createdProduct.Id));
        
        await Assert.That(deleteProductResult.Result.Type).IsEqualTo(ResultType.Result);
    }
    
    [Test]
    public async Task DeleteNonExistingUnit()
    {
        var deleteProductResult = await baseTest.MessageBus.InvokeAsync<UnitDeleted>(new DeleteUnit(Guid.NewGuid()));
        
        await Assert.That(deleteProductResult.Result.Type).IsEqualTo(ResultType.Error);
    }
    
    [Test]
    public async Task GetUnitById()
    {
        var createProductCommand = UnitsFixtures.CreateUnit();
        var createdProduct = await baseTest.MessageBus.InvokeAsync<UnitCreated>(createProductCommand);
        
        var productFound = await baseTest.MessageBus.InvokeAsync<UnitFound>(new FindUnit(createdProduct.Id));
        
        await Assert.That(productFound.Product.Type).IsEqualTo(ResultType.Result);
    }
    
    [Test]
    public async Task GetNotExistingUnit()
    {
        var productFound = await baseTest.MessageBus.InvokeAsync<UnitFound>(new FindUnit(Guid.NewGuid()));
        
        await Assert.That(productFound.Product.Type).IsEqualTo(ResultType.Error);
    }
    
    [Test]
    public async Task GetUnits()
    {
        var createProductCommand = UnitsFixtures.CreateUnit();
        var createdProduct = await baseTest.MessageBus.InvokeAsync<UnitCreated>(createProductCommand);
        
        var productsFound = await baseTest.MessageBus.InvokeAsync<UnitsFound>(new FindUnits());
        
        await Assert.That(productsFound.Products).IsNotEmpty();
        await Assert.That(productsFound.Products.Any(p => p.Id == createdProduct.Id)).IsTrue();
    }
}