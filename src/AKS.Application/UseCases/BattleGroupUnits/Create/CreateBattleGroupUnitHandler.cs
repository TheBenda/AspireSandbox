using AKS.Application.Mapping;
using AKS.Application.Mapping.BattleGroups;
using AKS.Application.Repositories;

namespace AKS.Application.UseCases.BattleGroupUnits.Create;

public static class CreateBattleGroupUnitHandler
{
    public static async Task<BattleGroupUnitCreated> Handle(CreateBattleGroupUnit command, IBattleGroupWriteRepository repository, CancellationToken ct)
    {
        var modifiedBattleGroupResult =
            await repository.AddUnitToBattleGroupAsync(command.BattleGroupId, command.UnitId, ct);
        return new BattleGroupUnitCreated(modifiedBattleGroupResult.ToDto(BattleGroupsExtensions.ToDto));
    }
}