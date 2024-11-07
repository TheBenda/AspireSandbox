namespace AKS.Application.UseCases.Toppings.Delete;

public record DeleteTopping(Guid ToppingId)
{
    public static DeleteTopping New(Guid toppingId) => new(toppingId);
}