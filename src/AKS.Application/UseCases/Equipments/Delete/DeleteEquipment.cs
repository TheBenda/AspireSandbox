namespace AKS.Application.UseCases.Equipments.Delete;

public record DeleteEquipment(Guid ProductId, Guid ToppingId)
{
    public static DeleteEquipment New(Guid productId, Guid toppingId) => new(productId, toppingId);
}