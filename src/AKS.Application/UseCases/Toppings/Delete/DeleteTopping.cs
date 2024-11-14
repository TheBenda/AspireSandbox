namespace AKS.Application.UseCases.Toppings.Delete;

public record DeleteTopping(Guid ProductId, Guid ToppingId)
{
    public static DeleteTopping New(Guid productId, Guid toppingId) => new(productId, toppingId);
}