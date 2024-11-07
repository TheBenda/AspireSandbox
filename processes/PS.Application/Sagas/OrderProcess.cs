using AKS.MessagingContracts.Command;

using Wolverine;

namespace PS.Application.Sagas;

public class OrderProcess : Saga
{
    public Guid OrderProcessId { get; set; }

    public static Task HandleStart(PrepareProcessingSaga processingSaga)
    {
        return Task.CompletedTask;
    }
}