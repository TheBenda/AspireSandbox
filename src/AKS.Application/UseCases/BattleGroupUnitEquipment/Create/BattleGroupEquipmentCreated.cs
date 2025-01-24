using AKS.Application.UseCases.BattleGroups.Transport;
using AKS.Domain.Results;

namespace AKS.Application.UseCases.BattleGroupUnitEquipment.Create;

public record BattleGroupEquipmentCreated(PersistenceResult<BattleGroupDto> Result);