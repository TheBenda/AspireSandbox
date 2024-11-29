namespace AKS.Application.UseCases.Units.Delete;

public record DeleteUnit(Guid UnitId)
{
    public static DeleteUnit New(Guid unitId) => new(unitId);
}