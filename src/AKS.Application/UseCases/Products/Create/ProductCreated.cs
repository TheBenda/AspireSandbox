namespace AKS.Application.UseCases.Products.Create;

public record ProductCreated(Guid ProductId, string Name, decimal Price);