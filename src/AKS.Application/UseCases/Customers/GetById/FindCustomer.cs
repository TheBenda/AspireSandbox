namespace AKS.Application.UseCases.Customers.GetById;

public record FindCustomer(Guid CustomerId)
{
    public static FindCustomer New(Guid id) => new (id);
}