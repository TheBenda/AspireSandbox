using AKS.Application.Mapping;
using AKS.Application.Mapping.Products;
using AKS.Application.Repositories;
using AKS.Domain.Entities;

namespace AKS.Application.UseCases.Toppings.Create;

public static class AddToppingToProductHandler
{
    public static async Task<AddedToppingToProduct> HandleAsync(AddToppingToProduct request, IProductWriteRepository productWriteRepository, CancellationToken cancellationToken)
    {
        var topping = new Topping() { Name = request.Name, Price = request.Price };
        
        var addedTopping = await productWriteRepository.CreateToppingToProductAsync(request.ProductId, topping, cancellationToken);

        return AddedToppingToProduct.New(addedTopping.ToDto(ProductsExtensions.ToDto));
    }
}