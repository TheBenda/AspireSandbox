using AKS.Application.Repositories;

namespace AKS.Application.UseCases.Equipments.Delete;

public static class DeleteEquipmentHandler
{
    public static async Task<EquipmentDeleted> Handle(DeleteEquipment request, IUnitWriteRepository unitWriteRepository, CancellationToken cancellationToken)
    {
        var deleteToppingFromProductResult = await unitWriteRepository.DeleteToppingAsync(request.ProductId, request.ToppingId, cancellationToken);
        return new EquipmentDeleted(deleteToppingFromProductResult);
    }
}