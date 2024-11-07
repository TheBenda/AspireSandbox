namespace AKS.MessagingContracts.Transport;

public record OrderDto(Guid CustomerId, List<ProductDto> Products)
{

}
