namespace AKS.MessagingContracts.Transport;

public record class OrderDto(Guid CustomerId, List<ProductDto> Products)
{

}
