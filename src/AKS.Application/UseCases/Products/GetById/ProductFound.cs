using AKS.Application.UseCases.Toppings.GetById;

namespace AKS.Application.UseCases.Products.GetById;

public record ProductFound(Guid ProductId, string Name, decimal Price, List<ToppingFound> Toppings);