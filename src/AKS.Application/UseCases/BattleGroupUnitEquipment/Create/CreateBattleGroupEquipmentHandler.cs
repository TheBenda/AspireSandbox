using AKS.Application.Mapping;
using AKS.Application.Mapping.BattleGroups;
using AKS.Application.Repositories;

namespace AKS.Application.UseCases.BattleGroupUnitEquipment.Create;

public static class CreateBattleGroupEquipmentHandler
{
    public static async Task<BattleGroupEquipmentCreated> Handle(CreateBattleGroupEquipment command,
        IBattleGroupWriteRepository repository, CancellationToken ct)
    {
        var result = await repository.AddToppingToProductsOrderAsync(command.BattleGroupId, command.BattleGroupUnitId, command.EquipmentId, ct);
        return new BattleGroupEquipmentCreated(result.ToDto(BattleGroupsExtensions.ToDto));
    }
}