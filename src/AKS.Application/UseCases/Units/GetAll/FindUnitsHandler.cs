using AKS.Application.Mapping.Units;
using AKS.Application.Repositories;

namespace AKS.Application.UseCases.Units.GetAll;

public static class FindUnitsHandler
{
    public static async Task<UnitsFound> HandleAsync(FindUnits request, IUnitReadRepository unitReadRepository, CancellationToken cancellationToken)
    {
        var products = await unitReadRepository.GetAllProductsAsync(cancellationToken);
        return new UnitsFound(products.ToDtoList());
    }
}