using AKS.Application.Mapping;
using AKS.Application.Mapping.Products;
using AKS.Application.Repositories;

namespace AKS.Application.UseCases.Products.GetById;

public static class FindProductHandler
{
    public static async Task<ProductFound> HandleAsync(FindProduct request, IProductReadRepository productReadRepository, CancellationToken cancellationToken)
    {
        var foundProduct = await productReadRepository.GetProductByIdAsync(request.productId, cancellationToken);
        return new ProductFound(foundProduct.ToDto(ProductsExtensions.ToDto));
    }
}