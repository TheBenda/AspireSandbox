using AKS.Application.Repositories;

namespace AKS.Application.UseCases.Toppings.Delete;

public static class DeleteToppingHandler
{
    public static async Task<ToppingDeleted> Handle(DeleteTopping request, IProductWriteRepository productWriteRepository, CancellationToken cancellationToken)
    {
        var deleteToppingFromProductResult = await productWriteRepository.DeleteToppingAsync(request.ProductId, request.ToppingId, cancellationToken);
        return new ToppingDeleted(deleteToppingFromProductResult);
    }
}