using AKS.Application.Repositories;

namespace AKS.Application.UseCases.Units.Delete;

public static class DeleteUnitHandler
{
    public static async Task<UnitDeleted> Handle(DeleteUnit request, IUnitWriteRepository writeRepository,
        CancellationToken ct)
    {
        var deletedCustomerResult = await writeRepository.DeleteProductAsync(request.UnitId, ct).ConfigureAwait(false);
        return new UnitDeleted(deletedCustomerResult);
    }
}