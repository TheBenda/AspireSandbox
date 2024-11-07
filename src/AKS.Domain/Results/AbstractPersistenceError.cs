namespace AKS.Domain;

public class AbstractPersistenceError(string message)
{
    public string Message { get; set; } = message;
}
