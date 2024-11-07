namespace AKS.Application.UseCases.Customers.GetById;

public record CustomerFound(Guid Id,
string FirstName,
string LastName,
string? Street,
string? City,
string? State,
string? Country,
string? ZipCode,
long Latitude,
long Longitude)
{
    
}