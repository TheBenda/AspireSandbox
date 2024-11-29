namespace AKS.Application.UseCases.Units.Create;

public record CreateUnit(string Name,
    string? Rule,
    int Health,
    int Attack,
    int Defense,
    int Movement,
    decimal Range,
    int Accuracy,
    decimal Points);