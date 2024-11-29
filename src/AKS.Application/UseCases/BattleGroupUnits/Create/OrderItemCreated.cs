using AKS.Application.UseCases.BattleGroups.Transport;
using AKS.Domain.Entities;
using AKS.Domain.Results;

namespace AKS.Application.UseCases.OrderItems.Create;

public record OrderItemCreated(PersistenceResult<BattleGroupDto> Order);