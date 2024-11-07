namespace AKS.Application.UseCases.Customers.Create;

public record CreateCustomer(
    string FirstName,
    string LastName,
    string? Street,
    string? City,
    string? State,
    string? Country,
    string? ZipCode,
    long Latitude,
    long Longitude);