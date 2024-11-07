using AKS.Domain.Results;
using AKS.Domain.Results.Status;

namespace AKS.Application.UseCases.Toppings.Delete;

public record ToppingDeleted(PersistenceResult<SuccsefullTransaction> Result);