using AKS.Application.UseCases.BattleGroups.Transport;
using AKS.Domain.Results;

namespace AKS.Application.UseCases.BattleGroupUnits.Create;

public record BattleGroupUnitCreated(PersistenceResult<BattleGroupDto> Order);