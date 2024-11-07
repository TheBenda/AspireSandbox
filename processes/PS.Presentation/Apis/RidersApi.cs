using PS.Application.UseCases.Riders.Create;
using PS.Application.UseCases.Riders.Update;

using Wolverine;

namespace PS.Presentation.Apis;

public static class RidersApi
{
    public static void MapRiderEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Rider").WithTags("Riders");

        group.MapPost("/", async (CreateRider createRider, IMessageBus messageBus, CancellationToken cancellationToken) =>
        {
            
        });

        group.MapPost("/location/{id}", async (UpdateRidersLocation updateRidersLocation, IMessageBus messageBus, CancellationToken cancellationToken) =>
        {
            
        });
    }
}