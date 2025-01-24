using AKS.Application.UseCases.Customers.Create;
using AKS.Application.UseCases.Customers.Delete;
using AKS.Application.UseCases.Customers.GetAll;
using AKS.Application.UseCases.Customers.GetById;
using AKS.Application.UseCases.Customers.Transport;
using AKS.Domain.Results;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Wolverine;

namespace AKS.Presentation.Apis;

public static class CustomerApi
{
    public static void MapCustomerEndpoints(this IEndpointRouteBuilder routes)
    {
        RouteGroupBuilder group = routes.MapGroup("/api/Customer").WithTags("Customer");

        group.MapGet("/",
                async Task<Results<Ok<CustomersFound>, ProblemHttpResult>> (IMessageBus messageBus,
                    CancellationToken cancellationToken) =>
                {
                    CustomersFound customersFound = await messageBus
                        .InvokeAsync<CustomersFound>(new FindCustomers(), cancellationToken).ConfigureAwait(false);
                    return TypedResults.Ok(customersFound);
                })
            .WithName("GetAllCustomers")
            .WithOpenApi();

        group.MapGet("/{id}",
                async Task<Results<Ok<CustomerDto>, NotFound>> (Guid id, IMessageBus messageBus,
                    CancellationToken cancellationToken) =>
                {
                    CustomerFound foundCustomerResult = await messageBus
                        .InvokeAsync<CustomerFound>(FindCustomer.New(id), cancellationToken).ConfigureAwait(false);

                    return foundCustomerResult.Customer.Type switch
                    {
                        ResultType.Result => TypedResults.Ok(foundCustomerResult.Customer.Result!.Value),
                        _ => TypedResults.NotFound()
                    };
                })
            .WithName("GetCustomerById")
            .WithOpenApi();

        group.MapPost("/",
                async (CreateCustomer createCustomerRequest, IMessageBus messageBus,
                    CancellationToken cancellationToken) =>
                {
                    CustomerCreated createdCustomer =
                        await messageBus.InvokeAsync<CustomerCreated>(createCustomerRequest, cancellationToken);
                    return TypedResults.Created($"/api/Customer/{createdCustomer.CustomerId}", createdCustomer);
                })
            .WithName("CreateCustomer")
            .WithOpenApi();

        group.MapDelete("/{id}",
                async Task<Results<Ok, NotFound<ProblemDetails>>> (Guid id, IMessageBus messageBus,
                    CancellationToken cancellationToken) =>
                {
                    CustomerDeleted customerDeleted =
                        await messageBus.InvokeAsync<CustomerDeleted>(DeleteCustomer.New(id), cancellationToken);
                    return customerDeleted.Result.Type switch
                    {
                        ResultType.Result => TypedResults.Ok(),
                        _ => TypedResults.NotFound(new ProblemDetails
                        {
                            Title = "Customer not found",
                            Detail = customerDeleted.Result.Error?.Message,
                            Status = StatusCodes.Status404NotFound
                        })
                    };
                })
            .WithName("DeleteCustomer")
            .WithOpenApi();
    }
}