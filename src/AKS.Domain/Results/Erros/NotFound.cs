namespace AKS.Domain.Results.Erros;

public class NotFound(string message) : AbstractPersistenceError(message)
{
    public static NotFound Empty() =>
        new("");
}
