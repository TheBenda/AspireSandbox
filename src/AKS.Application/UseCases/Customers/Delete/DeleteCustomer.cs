namespace AKS.Application.UseCases.Customers.Delete;

public record DeleteCustomer(Guid CustomerId)
{
    public static DeleteCustomer New(Guid id) => new(id);
}