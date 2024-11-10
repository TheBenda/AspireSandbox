namespace AKS.Domain.Results.Errors;

public class NotFound(string message) : AbstractPersistenceError(message)
{
    public static NotFound Empty() =>
        new("");
    public static NotFound WithMessage(string message) => new(message);
}
