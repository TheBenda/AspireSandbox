using AKS.Application.Repositories;

namespace AKS.Application.UseCases.Products.Delete;

public static class DeleteProductHandler
{
    public static async Task<ProductDeleted> Handle(DeleteProduct request, IProductWriteRepository writeRepository,
        CancellationToken ct)
    {
        var deletedCustomerResult = await writeRepository.DeleteProductAsync(request.ProductId, ct).ConfigureAwait(false);
        return new ProductDeleted(deletedCustomerResult);
    }
}