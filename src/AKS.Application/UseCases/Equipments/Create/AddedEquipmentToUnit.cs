using AKS.Application.UseCases.Units.Transport;
using AKS.Domain.Results;

namespace AKS.Application.UseCases.Equipments.Create;

public record AddedEquipmentToUnit(PersistenceResult<UnitDto> Result)
{
    public static AddedEquipmentToUnit New(PersistenceResult<UnitDto> result) 
        => new AddedEquipmentToUnit(result);
}