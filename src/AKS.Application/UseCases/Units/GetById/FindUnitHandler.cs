using AKS.Application.Mapping;
using AKS.Application.Mapping.Units;
using AKS.Application.Repositories;

namespace AKS.Application.UseCases.Units.GetById;

public static class FindUnitHandler
{
    public static async Task<UnitFound> HandleAsync(FindUnit request, IUnitReadRepository unitReadRepository, CancellationToken cancellationToken)
    {
        var foundProduct = await unitReadRepository.GetProductByIdAsync(request.UnitId, cancellationToken);
        return new UnitFound(foundProduct.ToDto(UnitsExtensions.ToDto));
    }
}