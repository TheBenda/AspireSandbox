using AKS.Application.UseCases.Equipments.Transport;

namespace AKS.Application.UseCases.Units.Transport;

public record UnitDto(
    Guid Id,
    string Name,
    string? Rule,
    int Health,
    int Attack,
    int Defense,
    int Movement,
    decimal Range,
    int Accuracy,
    decimal Points,
    List<EquipmentDto> Equipments);