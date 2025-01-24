using AKS.Application.Repositories;
using AKS.Domain.Entities;

namespace AKS.Application.UseCases.BattleGroups.Create;

public static class CreateBattleGroupHandler
{
    public static async Task<BattleGroupCreated> Handle(CreateBattleGroup command, IBattleGroupWriteRepository repository, CancellationToken ct)
    {
        var battleGroup = new BattleGroup
        {
            GroupCreated = DateTime.UtcNow,
            GroupName = command.GroupName,
            CustomerId = command.OwnerId
        };
        var createdBattleGroup = await repository.CreateAsync(battleGroup, ct);
        return new BattleGroupCreated(createdBattleGroup.Id, createdBattleGroup.CustomerId, createdBattleGroup.GroupName, createdBattleGroup.GroupCreated);
    }
}