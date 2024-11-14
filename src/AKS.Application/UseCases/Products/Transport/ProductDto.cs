using AKS.Application.UseCases.Toppings.Transport;

namespace AKS.Application.UseCases.Products.Transport;

public record ProductDto(Guid Id, string Name, decimal Price, List<ToppingDto> Toppings);