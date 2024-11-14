using AKS.Application.Mapping.Products;
using AKS.Application.UseCases.Products.Create;
using AKS.Application.UseCases.Products.Delete;
using AKS.Application.UseCases.Products.GetAll;
using AKS.Application.UseCases.Products.GetById;
using AKS.Application.UseCases.Toppings.Create;
using AKS.Domain.Results;
using AKS.IntegrationTests.Fixtures.Products;

namespace AKS.IntegrationTests.Products;

[ClassDataSource<BaseIntegrationTest>(Shared = SharedType.PerAssembly)]
public class ProductTests(BaseIntegrationTest baseTest)
{
    [Test]
    public async Task CreateProduct()
    {
        var command = ProductsFixtures.CreateProduct();
        var createdProduct = await baseTest.MessageBus.InvokeAsync<ProductCreated>(command);
        
        await Assert.That(createdProduct.Name).IsEqualTo(command.Name);
        await Assert.That(createdProduct.Price).IsEqualTo(command.Price);
    }
    
    [Test]
    public async Task AddToppingToProduct()
    {
        var createProductCommand = ProductsFixtures.CreateProduct();
        var createdProduct = await baseTest.MessageBus.InvokeAsync<ProductCreated>(createProductCommand);
        
        var addToppingCommand = ProductsFixtures.AddTopping(createdProduct.ProductId);
        
        var addedTopicResult = await baseTest.MessageBus.InvokeAsync<AddedToppingToProduct>(addToppingCommand);
        
        await Assert.That(addedTopicResult.Result.Type).IsEqualTo(ResultType.Result);
        
        var addedToppic = addedTopicResult.Result.Result!.Value;

        await Assert.That(addedToppic.Toppings.Count).IsEqualTo(1);
        await Assert.That(addedToppic.Toppings[0].Name).IsEqualTo(addToppingCommand.Name);
        await Assert.That(addedToppic.Toppings[0].Price).IsEqualTo(addToppingCommand.Price);
    }

    [Test]
    public async Task AddToppingToNotExistingProduct()
    {
        var addToppingCommand = ProductsFixtures.AddTopping(Guid.NewGuid());
        
        var addedTopicResult = await baseTest.MessageBus.InvokeAsync<AddedToppingToProduct>(addToppingCommand);
        
        await Assert.That(addedTopicResult.Result.Type).IsEqualTo(ResultType.Error);
    }

    [Test]
    public async Task DeleteProduct()
    {
        var createProductCommand = ProductsFixtures.CreateProduct();
        var createdProduct = await baseTest.MessageBus.InvokeAsync<ProductCreated>(createProductCommand);
        
        var deleteProductResult = await baseTest.MessageBus.InvokeAsync<ProductDeleted>(new DeleteProduct(createdProduct.ProductId));
        
        await Assert.That(deleteProductResult.Result.Type).IsEqualTo(ResultType.Result);
    }
    
    [Test]
    public async Task DeleteNonExistingProduct()
    {
        var deleteProductResult = await baseTest.MessageBus.InvokeAsync<ProductDeleted>(new DeleteProduct(Guid.NewGuid()));
        
        await Assert.That(deleteProductResult.Result.Type).IsEqualTo(ResultType.Error);
    }
    
    [Test]
    public async Task GetProductById()
    {
        var createProductCommand = ProductsFixtures.CreateProduct();
        var createdProduct = await baseTest.MessageBus.InvokeAsync<ProductCreated>(createProductCommand);
        
        var productFound = await baseTest.MessageBus.InvokeAsync<ProductFound>(new FindProduct(createdProduct.ProductId));
        
        await Assert.That(productFound.Product.Type).IsEqualTo(ResultType.Result);
    }
    
    [Test]
    public async Task GetNotExistingProduct()
    {
        var productFound = await baseTest.MessageBus.InvokeAsync<ProductFound>(new FindProduct(Guid.NewGuid()));
        
        await Assert.That(productFound.Product.Type).IsEqualTo(ResultType.Error);
    }
    
    [Test]
    public async Task GetProducts()
    {
        var createProductCommand = ProductsFixtures.CreateProduct();
        var createdProduct = await baseTest.MessageBus.InvokeAsync<ProductCreated>(createProductCommand);
        
        var productsFound = await baseTest.MessageBus.InvokeAsync<ProductsFound>(new FindProducts());
        
        await Assert.That(productsFound.Products).IsNotEmpty();
        await Assert.That(productsFound.Products.Any(p => p.Id == createdProduct.ProductId)).IsTrue();
    }
}