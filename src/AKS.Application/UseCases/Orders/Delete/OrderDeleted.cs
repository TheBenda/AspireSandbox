using AKS.Domain.Results;
using AKS.Domain.Results.Status;

namespace AKS.Application.UseCases.Orders.Delete;

public record OrderDeleted(PersistenceResult<SuccsefullTransaction> Result)
{
    public static OrderDeleted New(PersistenceResult<SuccsefullTransaction> result) 
        => new(result);
}