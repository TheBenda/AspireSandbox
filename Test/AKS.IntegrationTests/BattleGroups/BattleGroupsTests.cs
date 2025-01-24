using System.Net;
using System.Net.Http.Json;

using AKS.Application.UseCases.BattleGroups.Create;
using AKS.Application.UseCases.Customers.Create;
using AKS.Application.UseCases.Units.Create;
using AKS.IntegrationTests.Fixtures.BattleGroups;
using AKS.IntegrationTests.Fixtures.Customers;
using AKS.IntegrationTests.Fixtures.Units;
using AKS.Presentation.Apis;

namespace AKS.IntegrationTests.BattleGroups;

[ClassDataSource<BaseIntegrationTest>(Shared = SharedType.PerAssembly)]
public class BattleGroupsTests(BaseIntegrationTest baseTest)
{
    private const string BasePath = "/api/BattleGroup";

    [Test]
    public async Task Should_return_201_when_create_battle_group()
    {
        CreateCustomer createCustomer = CustomersFixtures.CreateCustomer();
        CustomerCreated customer = await baseTest.MessageBus.InvokeAsync<CustomerCreated>(createCustomer);

        HttpClient client = baseTest.GetAdminClient();

        HttpResponseMessage response = await client.PostAsJsonAsync(BasePath + "/" + customer.CustomerId,
            BattleGroupsFixtures.CreateBattleGroupRequest());

        await Assert.That(response).IsNotNull();
        await Assert.That(response.IsSuccessStatusCode).IsTrue();
    }

    [Test]
    public async Task Should_return_ok_when_adding_unit_to_group()
    {
        BattleGroupCreated battleGroupCreated = await SetupBattleGroupForOwner();
        UnitCreated unitCreated = await SetupUnit();

        HttpClient client = baseTest.GetAdminClient();

        HttpResponseMessage response = await client.PostAsJsonAsync($"{BasePath}/{battleGroupCreated.GroupId}/add/unit",
            new AddUnitToBattleGroupRequest(unitCreated.Id));

        await Assert.That(response).IsNotNull();
        await Assert.That(response.IsSuccessStatusCode).IsTrue();
    }

    [Test]
    public async Task Should_return_404_when_adding_not_existing_unit_to_group()
    {
        BattleGroupCreated battleGroupCreated = await SetupBattleGroupForOwner();

        HttpClient client = baseTest.GetAdminClient();

        HttpResponseMessage response = await client.PostAsJsonAsync($"{BasePath}/{battleGroupCreated.GroupId}/add/unit",
            new AddUnitToBattleGroupRequest(Guid.NewGuid()));

        await Assert.That(response).IsNotNull();
        await Assert.That(response.StatusCode).IsEqualTo(HttpStatusCode.NotFound);
    }

    private async Task<BattleGroupCreated> SetupBattleGroupForOwner()
    {
        CreateCustomer createCustomer = CustomersFixtures.CreateCustomer();
        CustomerCreated customer = await baseTest.MessageBus.InvokeAsync<CustomerCreated>(createCustomer);

        CreateBattleGroupRequest request = BattleGroupsFixtures.CreateBattleGroupRequest();

        BattleGroupCreated createdGroup = await baseTest.MessageBus.InvokeAsync<BattleGroupCreated>(
            new CreateBattleGroup(customer.CustomerId,
                request.GroupName));

        return createdGroup;
    }

    private async Task<UnitCreated> SetupUnit()
    {
        return await baseTest.MessageBus.InvokeAsync<UnitCreated>(UnitsFixtures.CreateUnit());
    }
}