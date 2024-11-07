namespace AKS.Application.UseCases.Customers.Create;

public record CustomerCreated(
    Guid CustomerId,
    string FirstName,
    string LastName,
    string? Street,
    string? City,
    string? State,
    string? Country,
    string? ZipCode,
    long Latitude,
    long Longitude);