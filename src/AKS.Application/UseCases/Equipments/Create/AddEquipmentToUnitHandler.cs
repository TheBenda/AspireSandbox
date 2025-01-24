using AKS.Application.Mapping;
using AKS.Application.Mapping.Units;
using AKS.Application.Repositories;
using AKS.Domain.Entities;

namespace AKS.Application.UseCases.Equipments.Create;

public static class AddEquipmentToUnitHandler
{
    public static async Task<AddedEquipmentToUnit> HandleAsync(AddEquipmentToUnit request, IUnitWriteRepository unitWriteRepository, CancellationToken cancellationToken)
    {
        var topping = new Equipment
        {
            Name = request.Name,
            Rule = request.Rule,
            Attack = request.Attack,
            Points = request.Points
        };
        
        var addedTopping = await unitWriteRepository.CreateToppingToProductAsync(request.UnitId, topping, cancellationToken);

        return AddedEquipmentToUnit.New(addedTopping.ToDto(UnitsExtensions.ToDto));
    }
}