namespace AKS.Domain.Values;

public class OrderedToppingItem
{
    public Guid ToppingId { get; init; }
    public required string Name { get; init; }
}