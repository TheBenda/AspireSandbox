using AKS.Presentation.Apis;

using Bogus;

namespace AKS.IntegrationTests.Fixtures.BattleGroups;

public static class BattleGroupsFixtures
{
    private static readonly Faker Faker = new();

    public static CreateBattleGroupRequest CreateBattleGroupRequest()
    {
        return new CreateBattleGroupRequest(Faker.Name.FirstName());
    }
}