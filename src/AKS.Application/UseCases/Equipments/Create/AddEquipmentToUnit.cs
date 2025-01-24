namespace AKS.Application.UseCases.Equipments.Create;

public record AddEquipmentToUnit(Guid UnitId,
    string Name,
    string? Rule,
    int? Attack,
    decimal Points);