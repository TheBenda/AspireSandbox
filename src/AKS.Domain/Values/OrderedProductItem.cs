namespace AKS.Domain.Values;

public class OrderedProductItem
{
    public Guid ProductId { get; init; }
    public required string Name { get; init; }
}