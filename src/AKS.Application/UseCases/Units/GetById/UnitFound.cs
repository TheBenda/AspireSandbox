using AKS.Application.UseCases.Units.Transport;
using AKS.Domain.Results;

namespace AKS.Application.UseCases.Units.GetById;

public record UnitFound(PersistenceResult<UnitDto> Product);