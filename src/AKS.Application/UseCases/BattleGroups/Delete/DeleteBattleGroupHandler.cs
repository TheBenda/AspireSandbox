using AKS.Application.Repositories;
using AKS.Domain.Results;
using AKS.Domain.Results.Status;

namespace AKS.Application.UseCases.BattleGroups.Delete;

public static class DeleteBattleGroupHandler
{
    public static async Task<BattleGroupDeleted> Handle(DeleteBattleGroup command,
        IBattleGroupWriteRepository battleGroupWriteRepository, CancellationToken ct)
    {
        PersistenceResult<SuccsefullTransaction>? result =
            await battleGroupWriteRepository.DeleteAsync(command.BattleGroupId, ct).ConfigureAwait(false);
        return new BattleGroupDeleted(result);
    }
}