using System.Net.Http.Json;

using AKS.Application.UseCases.Customers.Create;
using AKS.IntegrationTests.Fixtures.Customers;

namespace AKS.IntegrationTests.Customers;

[ClassDataSource<BaseIntegrationTest>(Shared = SharedType.PerAssembly)]
public class CustomersApiTests(BaseIntegrationTest baseTest)
{
    private const string BasePath = "/api/Customer";
    
    [Test]
    public async Task CreateCustomer()
    {
        var client = baseTest.GetAdminClient();
    
        var response = await client.PostAsJsonAsync(BasePath + "/", CustomersFixtures.CreateCustomer());
        
        await Assert.That(response).IsNotNull();
        await Assert.That(response.IsSuccessStatusCode).IsTrue();
    }
    
    [Test]
    public async Task FindCustomer()
    {
        var client = baseTest.GetAdminClient();
        
        var customerCreated = await baseTest.MessageBus.InvokeAsync<CustomerCreated>(CustomersFixtures.CreateCustomer());
    
        var response = await client.GetAsync(BasePath + $"/{customerCreated.CustomerId}");
        
        await Assert.That(response).IsNotNull();
        await Assert.That(response.IsSuccessStatusCode).IsTrue();
    }
    
    [Test]
    public async Task DeleteCustomer()
    {
        var client = baseTest.GetAdminClient();
    
        var customerCreated = await baseTest.MessageBus.InvokeAsync<CustomerCreated>(CustomersFixtures.CreateCustomer());
        
        var response = await client.DeleteAsync(BasePath + $"/{customerCreated.CustomerId}");
        
        await Assert.That(response).IsNotNull();
        await Assert.That(response.IsSuccessStatusCode).IsTrue();
    }

    [Test]
    public async Task GetAllCustomers()
    {
        var client = baseTest.GetAdminClient();

        var response = await client.GetAsync(BasePath + "/");

        await Assert.That(response).IsNotNull();
        await Assert.That(response.IsSuccessStatusCode).IsTrue();
    }
}