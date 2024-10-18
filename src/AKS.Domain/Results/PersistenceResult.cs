using System.Runtime.CompilerServices;

namespace AKS.Domain.Results;

public class PersistenceResult<T>
    // where T : class?
{
    public TypedResult<T>? Result { get; set; }
    public AbstractPersistenceError? Error { get; set; }
    public required ResultType Type { get; set; }

    public static PersistenceResult<T> Success(TypedResult<T> result)
    {
        return new PersistenceResult<T>
            {
                Result = result,
                Type = ResultType.Result
            };
    }

    public static PersistenceResult<T> Failure(AbstractPersistenceError error)
    {
        return new PersistenceResult<T>
            {
                Error = error,
                Type = ResultType.Error
            };
    }
}

public class TypedResult<T>
    //where T : notnull
{
    public required T Value {get; set;}

    public static TypedResult<T> Of(T value)
    {
        return new TypedResult<T>{
            Value = value
        };
    }
}
