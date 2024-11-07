using AKS.Domain.Results;
using AKS.Domain.Results.Status;

namespace AKS.Application.UseCases.Customers.Delete;

public record CustomerDeleted(PersistenceResult<SuccsefullTransaction> Result)
{
    public static CustomerDeleted New(PersistenceResult<SuccsefullTransaction> result) 
        => new (result);
}