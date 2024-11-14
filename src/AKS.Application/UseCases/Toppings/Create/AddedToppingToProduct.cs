using AKS.Application.UseCases.Products.Transport;
using AKS.Domain.Entities;
using AKS.Domain.Results;

namespace AKS.Application.UseCases.Toppings.Create;

public record AddedToppingToProduct(PersistenceResult<ProductDto> Result)
{
    public static AddedToppingToProduct New(PersistenceResult<ProductDto> result) 
        => new AddedToppingToProduct(result);
}