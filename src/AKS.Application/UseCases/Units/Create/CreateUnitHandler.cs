using AKS.Application.Repositories;
using AKS.Domain.Entities;

namespace AKS.Application.UseCases.Units.Create;

public static class CreateUnitHandler
{
    public static async Task<UnitCreated> HandleAsync(CreateUnit request, IUnitWriteRepository unitWriteRepository, CancellationToken cancellationToken)
    {
        var product = new Unit
        {
            Name = request.Name,
            Rule = request.Rule,
            Health = request.Health,
            Attack = request.Attack,
            Defense = request.Defense,
            Movement = request.Movement,
            Range = request.Range,
            Accuracy = request.Accuracy,
            Points = request.Points
        };
        
        var createdProduct = await unitWriteRepository.CreateProductAsync(product, cancellationToken);

        return new UnitCreated(createdProduct.Id, 
            createdProduct.Name, 
            createdProduct.Rule,
            createdProduct.Health,
            createdProduct.Attack,
            createdProduct.Defense,
            createdProduct.Movement,
            createdProduct.Range,
            createdProduct.Accuracy,
            createdProduct.Points);
    }
}