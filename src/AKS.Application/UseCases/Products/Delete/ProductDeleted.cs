using AKS.Domain.Results;
using AKS.Domain.Results.Status;

namespace AKS.Application.UseCases.Products.Delete;

public record ProductDeleted(PersistenceResult<SuccsefullTransaction> Result);