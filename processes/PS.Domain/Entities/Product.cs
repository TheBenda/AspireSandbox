namespace PS.Domain.Sagas;

public class Product
{
    public Guid Id { get; init; }
    public Guid OrderId { get; init; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public IEnumerable<Topping> Toppings { get; set; } = new List<Topping>();
}