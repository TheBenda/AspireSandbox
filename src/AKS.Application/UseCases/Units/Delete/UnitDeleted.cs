using AKS.Domain.Results;
using AKS.Domain.Results.Status;

namespace AKS.Application.UseCases.Units.Delete;

public record UnitDeleted(PersistenceResult<SuccsefullTransaction> Result);