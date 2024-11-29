namespace AKS.Application.UseCases.Equipments.Transport;

public record EquipmentDto(
    Guid Id, 
    string Name,
    string? Rule,
    int? Attack,
    decimal Points);