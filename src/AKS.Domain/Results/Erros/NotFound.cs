namespace AKS.Domain.Results.Erros;

public class NotFound : AbstractPersistenceError
{
    public static NotFound Empty() =>
        new();
}
