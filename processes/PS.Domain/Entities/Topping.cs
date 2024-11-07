namespace PS.Domain.Sagas;

public class Topping
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public Guid ProductId { get; set; }
}