using AKS.Application.UseCases.Products.Transport;
using AKS.Application.UseCases.Toppings.Transport;
using AKS.Domain.Entities;

namespace AKS.Application.Mapping.Products;

public static class ProductsExtensions
{
    public static ProductDto ToDto(this Product product)
        => new ProductDto(product.Id, product.Name, product.Price, product.Toppings.ToDtoList());
    
    public static List<ProductDto> ToDtoList(this List<Product> products)
        =>  products.Select(t => t.ToDto()).ToList();

    private static ToppingDto ToDto(this Topping topping)
        => new ToppingDto(topping.Id, topping.Name, topping.Price);


    private static List<ToppingDto> ToDtoList(this ICollection<Topping>? toppings)
    {
        return toppings is null ? [] : toppings.Select(t => t.ToDto()).ToList();
    }
    
}