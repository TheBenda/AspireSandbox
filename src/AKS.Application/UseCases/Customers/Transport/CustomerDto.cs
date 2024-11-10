namespace AKS.Application.UseCases.Customers.Transport;

public record CustomerDto(Guid Id,
    string FirstName,
    string LastName,
    string? Street,
    string? City,
    string? State,
    string? Country,
    string? ZipCode,
    long Latitude,
    long Longitude);