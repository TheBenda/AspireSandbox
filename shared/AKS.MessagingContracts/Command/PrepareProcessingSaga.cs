using AKS.MessagingContracts.Transport;

namespace AKS.MessagingContracts.Command;

public record class PrepareProcessingSaga(OrderDto Order)
{

}
