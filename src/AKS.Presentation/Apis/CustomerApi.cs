using AKS.Domain.Entities;
using AKS.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AKS.Presentation.Apis;

public static class CustomerApi
{
    public static void MapCustomerEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Customer").WithTags(nameof(Customer));

        group.MapGet("/", async (PrimaryDbContext db) =>
        {
        })
        .WithName("GetAllCustomers")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Customer>, NotFound>> (Guid id, PrimaryDbContext db) =>
        {
            return TypedResults.NotFound();
        })
        .WithName("GetCustomerById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (Guid id, Customer customer, PrimaryDbContext db) =>
        {
            return TypedResults.NotFound();
        })
        .WithName("UpdateCustomer")
        .WithOpenApi();

        group.MapPost("/", async (Customer customer, PrimaryDbContext db) =>
        {
            return TypedResults.Created($"/api/Customer/{customer.Id}",customer);
        })
        .WithName("CreateCustomer")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (Guid id, PrimaryDbContext db) =>
        {
            return TypedResults.NotFound();
        })
        .WithName("DeleteCustomer")
        .WithOpenApi();
    }
}
