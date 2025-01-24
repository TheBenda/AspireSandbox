using AKS.Domain.Results;
using AKS.Domain.Results.Status;

namespace AKS.Application.UseCases.Equipments.Delete;

public record EquipmentDeleted(PersistenceResult<SuccsefullTransaction> Result);