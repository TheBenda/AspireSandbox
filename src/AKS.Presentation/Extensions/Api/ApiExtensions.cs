using AKS.Presentation.Apis;

namespace AKS.Presentation.Extensions.Api;

internal static class ApiExtensions
{
    internal static void MapEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapCustomerEndpoints();
        routes.MapOrderEndpoints();
        routes.MapUnitEndpoints();
        routes.MapPaymentEndpoints();
        routes.MapEquipmentEndpoints();
    }
}