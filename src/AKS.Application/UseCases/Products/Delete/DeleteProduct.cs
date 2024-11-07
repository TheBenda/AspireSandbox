namespace AKS.Application.UseCases.Products.Delete;

public record DeleteProduct(Guid ProductId)
{
    public static DeleteProduct New(Guid productId) => new(productId);
}