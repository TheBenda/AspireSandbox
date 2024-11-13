namespace AKS.Domain.Values;

public class Address
{
    public string? Street { get; init; }
    public string? City { get; init; }
    public string? State { get; init; }
    public string? Country { get; init; }
    public string? ZipCode { get; init; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }
}