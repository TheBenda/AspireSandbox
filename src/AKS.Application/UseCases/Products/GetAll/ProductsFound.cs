using AKS.Application.UseCases.Products.Transport;

namespace AKS.Application.UseCases.Products.GetAll;

public record ProductsFound(List<ProductDto> Products);