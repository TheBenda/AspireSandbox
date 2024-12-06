using AKS.Domain.Results;
using AKS.Domain.Results.Status;

namespace AKS.Application.UseCases.BattleGroups.Delete;

public record BattleGroupDeleted(PersistenceResult<SuccsefullTransaction> Result)
{
    public static BattleGroupDeleted New(PersistenceResult<SuccsefullTransaction> result)
    {
        return new BattleGroupDeleted(result);
    }
}