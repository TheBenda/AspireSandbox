namespace AKS.Application.UseCases.BattleGroups.Create;

public record BattleGroupCreated(
    Guid GroupId,
    Guid OwnerId,
    string GroupName,
    DateTime CreationDate);