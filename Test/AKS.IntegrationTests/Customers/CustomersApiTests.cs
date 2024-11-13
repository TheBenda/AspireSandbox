namespace AKS.IntegrationTests.Customers;

[ClassDataSource<BaseIntegrationTest>(Shared = SharedType.PerAssembly)]
public class CustomersApiTests(BaseIntegrationTest baseTest)
{
    private const string BasePath = "/api/Customer";
    // [Test]
    // public async Task CreateCustomer()
    // {
    //     var client = baseTest.GetAdminClient();
    //
    //     await client.PostAsync();
    // }

    [Test]
    public async Task GetAllCustomers()
    {
        var client = baseTest.GetAdminClient();

        var response = await client.GetAsync(BasePath + "/");

        await Assert.That(response).IsNotNull();
    }
}