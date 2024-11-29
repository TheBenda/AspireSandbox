using AKS.Application.UseCases.BattleGroupUnits.Transport;

namespace AKS.Application.UseCases.BattleGroups.Transport;

public record BattleGroupDto(
    Guid Id,
    DateTime GroupCreated,
    string GroupName,
    List<BattleGroupUnitDto> Units);