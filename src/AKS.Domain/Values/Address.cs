namespace AKS.Domain.Values;

public class Address
{
    public string? Street { get; init; }
    public string? City { get; init; }
    public string? State { get; init; }
    public string? Country { get; init; }
    public string? ZipCode { get; init; }
    public long Latitude { get; init; }
    public long Longitude { get; init; }
}