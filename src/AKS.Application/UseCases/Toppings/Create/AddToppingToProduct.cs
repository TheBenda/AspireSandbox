namespace AKS.Application.UseCases.Toppings.Create;

public record AddToppingToProduct(Guid ProductId, string Name, decimal Price);