using AKS.Application.Repositories;
using AKS.Domain.Entities;

namespace AKS.Application.UseCases.Products.Create;

public static class CreateProductHandler
{
    public static async Task<ProductCreated> HandleAsync(CreateProduct request, IProductWriteRepository productWriteRepository, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
        };
        
        var createdProduct = await productWriteRepository.CreateProductAsync(product, cancellationToken);

        return new ProductCreated(createdProduct.Id, createdProduct.Name, createdProduct.Price);
    }
}