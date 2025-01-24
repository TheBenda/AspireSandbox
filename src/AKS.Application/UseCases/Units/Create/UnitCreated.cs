namespace AKS.Application.UseCases.Units.Create;

public record UnitCreated(Guid Id,
    string Name,
    string? Rule,
    int Health,
    int Attack,
    int Defense,
    int Movement,
    decimal Range,
    int Accuracy,
    decimal Points);