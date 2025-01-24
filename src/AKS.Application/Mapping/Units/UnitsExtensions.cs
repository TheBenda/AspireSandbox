using AKS.Application.UseCases.Equipments.Transport;
using AKS.Application.UseCases.Units.Transport;
using AKS.Domain.Entities;

namespace AKS.Application.Mapping.Units;

public static class UnitsExtensions
{
    public static UnitDto ToDto(this Unit unit)
        => new UnitDto(unit.Id, 
            unit.Name, 
            unit.Rule,
            unit.Health,
            unit.Attack,
            unit.Defense,
            unit.Movement,
            unit.Range,
            unit.Accuracy,
            unit.Points,
            unit.Equipments.ToDtoList());
    
    public static List<UnitDto> ToDtoList(this List<Unit> products)
        =>  products.Select(t => t.ToDto()).ToList();

    private static EquipmentDto ToDto(this Equipment equipment)
        => new EquipmentDto(
            equipment.Id, 
            equipment.Name, 
            equipment.Rule,
            equipment.Attack,
            equipment.Points);


    private static List<EquipmentDto> ToDtoList(this ICollection<Equipment>? toppings)
        => toppings is null ? [] : toppings.Select(t => t.ToDto()).ToList();
    
    
}