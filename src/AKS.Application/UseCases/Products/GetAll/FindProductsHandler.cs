using AKS.Application.Mapping.Products;
using AKS.Application.Repositories;

namespace AKS.Application.UseCases.Products.GetAll;

public static class FindProductsHandler
{
    public static async Task<ProductsFound> HandleAsync(FindProducts request, IProductReadRepository productReadRepository, CancellationToken cancellationToken)
    {
        var products = await productReadRepository.GetAllProductsAsync(cancellationToken);
        return new ProductsFound(products.ToDtoList());
    }
}